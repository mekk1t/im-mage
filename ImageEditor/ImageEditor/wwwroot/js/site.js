function toggleElement(id) {
    let element = document.getElementById(id);
    element.classList.toggle("w3-hide");
}

function calculateImageSize() {
    let currentSize = document.getElementById("zoom-input");
    let sizeOutput = document.getElementById("image-size");
    sizeOutput.value = `${currentSize.value}%`;
}
function toggleButtonSelectionStatus(button) {
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