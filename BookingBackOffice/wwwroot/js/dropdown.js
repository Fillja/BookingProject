const openDropdown = (listId) => {
    const dropdown = document.getElementById(`dropdown-${listId}`);

    if (dropdown.classList.contains('hidden')) {
        dropdown.classList.remove('hidden');
    }
    else {
        dropdown.classList.add('hidden');
    }
}