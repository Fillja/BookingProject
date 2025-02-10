using Infrastructure.Contexts;
using Infrastructure.Factories;
using Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public abstract class BaseRepository<TEntity>(DataContext context) where TEntity : class
{
    private readonly DataContext _context = context;

    public virtual async Task<ResponseResult> CreateAsync(TEntity entity)
    {
        try
        {
            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();

            return ResponseFactory.Created(entity, "Successfully created.");
        }
        catch (Exception ex)
        {
            return ResponseFactory.BadRequest(ex.Message);
        }
    }

    public virtual async Task<ResponseResult> GetAllAsync()
    {
        try
        {
            IEnumerable<TEntity> entityList = await _context.Set<TEntity>().ToListAsync();
            if (!entityList.Any())
                return ResponseFactory.NotFound("List is empty.");

            return ResponseFactory.Ok(entityList, "List was found.");

        }
        catch (Exception ex)
        {
            return ResponseFactory.BadRequest(ex.Message);
        }
    }

    public virtual async Task<ResponseResult> GetOneAsync(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            var entity = await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);
            if (entity == null)
                return ResponseFactory.NotFound("Entity could not be found.");

            return ResponseFactory.Ok(entity, "Entity found.");
        }
        catch (Exception ex)
        {
            return ResponseFactory.BadRequest(ex.Message);
        }
    }

    public virtual async Task<ResponseResult> UpdateAsync(TEntity entity)
    {
        try
        {
            _context.Entry(entity).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();

            return ResponseFactory.Ok(entity, "Successfully updated.");
        }
        catch (Exception ex)
        {
            return ResponseFactory.BadRequest(ex.Message);
        }
    }

    public virtual async Task<ResponseResult> DeleteAsync(TEntity entity)
    {
        try
        {
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();

            return ResponseFactory.Ok("Successfully deleted.");
        }
        catch (Exception ex)
        {
            return ResponseFactory.BadRequest(ex.Message);
        }
    }

    public virtual async Task<ResponseResult> DeleteAsync(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            var result = await GetOneAsync(predicate);
            if (result.StatusCode == StatusCode.OK)
            {
                var entity = (TEntity)result.Content!;

                _context.Set<TEntity>().Remove(entity);
                await _context.SaveChangesAsync();

                return ResponseFactory.Ok("Successfully deleted.");
            }
            else if (result.StatusCode == StatusCode.NOT_FOUND)
                return ResponseFactory.NotFound("No entity found.");

            return ResponseFactory.BadRequest(result.Message!);
        }
        catch (Exception ex)
        {
            return ResponseFactory.BadRequest(ex.Message);
        }
    }

    public virtual async Task<ResponseResult> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            var existResult = await _context.Set<TEntity>().AnyAsync(predicate);
            if (existResult)
                return ResponseFactory.Exists();

            return ResponseFactory.NotFound();
        }
        catch (Exception ex)
        {
            return ResponseFactory.BadRequest(ex.Message);
        }
    }
}