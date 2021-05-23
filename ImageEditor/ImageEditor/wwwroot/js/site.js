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
                let originalBase64 = document.getElementById('image-base64-hidden').value;
                $("#image-original").val(originalBase64);
                let contentType = document.getElementById('image-contentType-hidden').value;
                $("#content-type-original").val(contentType);
            },
            error: function (xhr) {
                alert(xhr.responseText);
            }
        }
    );
}

function getImageObject() {
    let result = {
        Base64: document.getElementById('image-base64-hidden').value,
        ContentType: document.getElementById('image-contentType-hidden').value
    };
    return result;
}

function rotateImage(rotate) {
    let image = getImageObject();
    $.ajax({
        url: "/api/images/transformations/rotation",
        type: 'POST',
        data: JSON.stringify({
            Image: image,
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

function flipImage(flip) {
    let image = getImageObject();
    $.ajax({
        url: "/api/images/transformations/flip",
        type: 'POST',
        data: JSON.stringify({
            Image: image,
            Flip: flip
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

function changeBrightness() {
    let image = getImageObject();
    $.ajax({
        url: "/api/images/transformations/brightness",
        type: 'POST',
        data: JSON.stringify({
            Image: image,
            BrightnessLevel: $("#brightness input").val()
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

function changeContrast() {
    let image = getImageObject();
    $.ajax({
        url: "/api/images/transformations/contrast",
        type: 'POST',
        data: JSON.stringify({
            Image: image,
            ContrastLevel: $("#contrast input").val()
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

function reset() {
    let contentType = $("#content-type-original").val();
    let imageBase64 = $("#image-original").val();
    let img = document.querySelector("img");
    img.src = `data:${contentType};base64, ${imageBase64}`;
}