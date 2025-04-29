document.querySelectorAll('.table').forEach(table => {
    let isDragging = false;

    table.addEventListener('mousedown', () => {
        isDragging = true;
        table.style.zIndex = 1000;
    });

    document.addEventListener('mousemove', (e) => {
        if (!isDragging) return;

        const parent = document.querySelector('.restaurant-image');
        const rect = parent.getBoundingClientRect();
        const x = ((e.clientX - rect.left) / rect.width) * 100;
        const y = ((e.clientY - rect.top) / rect.height) * 100;

        table.style.left = `${x}%`;
        table.style.top = `${y}%`;

        // Update hidden inputs
        const id = table.id.replace('table-', '');
        document.getElementById(`top-${id}`).value = y.toFixed(3);
        document.getElementById(`left-${id}`).value = x.toFixed(3);
    });

    document.addEventListener('mouseup', () => {
        isDragging = false;
    });
});