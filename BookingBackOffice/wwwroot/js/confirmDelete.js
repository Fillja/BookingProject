const confirmDelete = (listID, event) => {
    event.preventDefault();
    const confirmation = confirm("Are you sure you want to delete this booking?");

    if (confirmation) {
        const form = document.getElementById('bookingForm-' + listID);

        // Set the action to 'delete' and submit the form
        form.querySelector('input[name="action"]').value = 'delete';
        form.submit(); 
    }
}