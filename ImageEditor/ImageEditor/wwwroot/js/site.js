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