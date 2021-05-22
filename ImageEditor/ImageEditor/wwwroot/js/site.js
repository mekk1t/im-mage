function toggleElement(id) {
    let element = document.getElementById(id);
    element.classList.toggle("w3-hide");
}

function calculateInput(containerId) {
    let container = document.getElementById(containerId);
    let currentSize = container.querySelector('input');
    let sizeOutput = container.querySelector('output');
    sizeOutput.value = `${currentSize.value}%`;
}
function toggleFilterButtonSelectionStatus(button) {
    button.classList.toggle("selected");
    let applyButton = document.getElementById("apply-filters-button");
    if (applyButton.classList.contains("w3-disabled")) {
        applyButton.classList.toggle("w3-disabled");
    }
}

function toggleNonFilterButtonSelectionStatus(button) {
    button.classList.toggle("selected");
}

function toggleFilters() {
    let toggleButton = document.getElementById("toggle-more-filters-button");
    let moreFiltersDiv = document.getElementById("more-filters");
    if (moreFiltersDiv.classList.contains("w3-hide")) {
        toggleButton.textContent = "Спрятать";
    } else {
        toggleButton.textContent = "Показать другие";
    }

    moreFiltersDiv.classList.toggle("w3-hide");
}

function rotateImage() {
    let image = $("img");

}

function selectImage(form) {
    var input = document.getElementById("image-upload");
    var files = input.files;
    var formData = new FormData();

    formData.append("file", files[0]);

    $.ajax(
        {
            url: "/",
            type: 'POST',
            data: formData,
            processData: false,
            contentType: false,
            success: function (result) {
                $("div.editing-image").html(result);
            },
            error: function (xhr) {
                alert(xhr.responseText);
            }
        }
    );
}

function toggleModal(modalId) {
    let modal = document.getElementById(modalId);
    if (modal.style.display === 'none') {
        modal.style.display = 'block';
    } else {
        modal.style.display = 'none';
    }
}