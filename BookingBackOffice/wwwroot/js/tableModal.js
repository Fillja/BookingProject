function closeModal() {
    document.getElementById("editTableModal").style.display = "none";
}

function openModal(table) {
    const id = table.querySelector('input[name$=".Id"]').value;
    const name = table.querySelector('input[name$=".Name"]').value;
    const size = table.querySelector('input[name$=".Size"]').value;
    const top = table.querySelector('input[name$=".TopAlignment"]').value;
    const left = table.querySelector('input[name$=".LeftAlignment"]').value;

    document.getElementById("edit-table-id").value = id;
    document.getElementById("edit-table-name").value = name;
    document.getElementById("edit-table-size").value = size;
    document.getElementById("edit-table-top").value = top;
    document.getElementById("edit-table-left").value = left;

    document.getElementById("editTableModal").style.display = "flex";
}

// Optional: Close modal when clicking outside content
window.onclick = function (event) {
    const modal = document.getElementById("editTableModal");
    if (event.target === modal) {
        closeModal();
    }
}