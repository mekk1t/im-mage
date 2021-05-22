﻿function toggleElement(id) {
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

function selectImage() {
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
                document.getElementById('upload-image-modal').style.display = 'none';
            },
            error: function (xhr) {
                alert(xhr.responseText);
            }
        }
    );
}

function getImageObject() {
    let result = {
        Image: document.getElementById('image-base64-hidden').value,
        ContentType: document.getElementById('image-contentType-hidden').value
    };
    return result;
}

function rotateImage() {
    let rotate = 'UpsideDown';
    let image = getImageObject();
    $.ajax({
        url: "/api/images/transformations/rotate",
        type: 'POST',
        data: JSON.stringify({
            ImageBase64: image.Image,
            ContentType: image.ContentType,
            Rotation: rotate
        }),
        contentType: 'application/json',
        success: function (result) {
            $("div.editing-image").html(result);
        },
        error: function (xhr) {
            alert(xhr.responseText);
        }
    });
}