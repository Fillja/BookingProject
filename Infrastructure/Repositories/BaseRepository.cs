﻿using Infrastructure.Contexts;
using Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public abstract class BaseRepository<TEntity>(DataContext context) where TEntity : class
{
    private readonly DataContext _context = context;

    /*
        OK = 0,
        FAILED = 1,
        NOT_FOUND = 2,
        CONFLICT = 3
     */

    public virtual async Task<ResponseResult> CreateAsync(TEntity entity)
    {
        try
        {
            _context.Add(entity);
            await _context.SaveChangesAsync();

            return ResponseResult.Result(0, "Successfully created.", entity);
        }
        catch (Exception ex)
        {
            return ResponseResult.Result(1, ex.Message);
        }
    }

    public virtual async Task<ResponseResult> CreateMultipleAsync(IEnumerable<TEntity> entityList)
    {
        try
        {
            foreach (var entity in entityList) 
            {
                _context.Add(entity);
            }
            var result = await _context.SaveChangesAsync();

            return ResponseResult.Result(0, $"Successfully created {result} entities.", entityList);
        }
        catch (Exception ex)
        {
            return ResponseResult.Result(1, ex.Message);
        }
    }

    public virtual async Task<ResponseResult> GetAllAsync()
    {
        try
        {
            IEnumerable<TEntity> entityList = await _context.Set<TEntity>().ToListAsync();
            if (!entityList.Any())
                return ResponseResult.Result(2, "List is empty");

            return ResponseResult.Result(0, "List was found.", entityList);

        }
        catch (Exception ex)
        {
            return ResponseResult.Result(1, ex.Message);
        }
    }

    public virtual async Task<ResponseResult> GetOneAsync(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            var entity = await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);
            if (entity == null)
                return ResponseResult.Result(2, "Entity could not be found.");

            return ResponseResult.Result(0, "Entity found.", entity);
        }
        catch (Exception ex)
        {
            return ResponseResult.Result(1, ex.Message);
        }
    }

    public virtual async Task<ResponseResult> UpdateAsync(TEntity entity)
    {
        try
        {
            _context.Entry(entity).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();

            return ResponseResult.Result(0, "Successfully updated.", entity);
        }
        catch (Exception ex)
        {
            return ResponseResult.Result(1, ex.Message);
        }
    }

    public virtual async Task<ResponseResult> DeleteAsync(TEntity entity)
    {
        try
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();

            return ResponseResult.Result(0, "Successfully deleted.");
        }
        catch (Exception ex)
        {
            return ResponseResult.Result(1, ex.Message);
        }
    }

    public virtual async Task<ResponseResult> DeleteAsync(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            var result = await GetOneAsync(predicate);
            if (result.HasFailed)
                return result;

    
            var entity = (TEntity)result.Content!;

            _context.Remove(entity);
            await _context.SaveChangesAsync();

            return ResponseResult.Result(0, "Successfully deleted.");

        }
        catch (Exception ex)
        {
            return ResponseResult.Result(1, ex.Message);
        }
    }

    public virtual async Task<ResponseResult> DeleteMultipleAsync(IEnumerable<TEntity> entityList)
    {
        try
        {
            foreach(var entity in entityList)
            {
                _context.Remove(entity);
            }
            await _context.SaveChangesAsync();

            return ResponseResult.Result(0, "Successfully deleted.");
        }
        catch (Exception ex)
        {
            return ResponseResult.Result(1, ex.Message);
        }
    }

    public virtual async Task<ResponseResult> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            var existResult = await _context.Set<TEntity>().AnyAsync(predicate);
            if (existResult)
                return ResponseResult.Result(3, "Entity already exists.");

            return ResponseResult.Result(0);
        }
        catch (Exception ex)
        {
            return ResponseResult.Result(1, ex.Message);
        }
    }
}