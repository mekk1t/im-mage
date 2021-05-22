function toggleElement(id) {
    let element = document.getElementById(id);
    element.classList.toggle("w3-hide");
}

function calculateImageSize() {
    let currentSize = document.getElementById("zoom-input");
    let sizeOutput = document.getElementById("image-size");
    sizeOutput.value = `${currentSize.value}%`;
}