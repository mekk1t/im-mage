var ORIGINAL_BASE_64 = "";
var CONTENT_TYPE = "";
var BRIGHTNESS_LEVEL = 0;
var CONTRAST_LEVEL = 0;


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
                ORIGINAL_BASE_64 = document.getElementById('image-base64-hidden').value;
                CONTENT_TYPE = document.getElementById('image-contentType-hidden').value;
                BRIGHTNESS_LEVEL = 100;
                CONTRAST_LEVEL = 100;
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
        ContentType: CONTENT_TYPE
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
    let brightness = $("#brightness input").val();
    $.ajax({
        url: "/api/images/transformations/brightness",
        type: 'POST',
        data: JSON.stringify({
            Image: { Base64: ORIGINAL_BASE_64, ContentType: CONTENT_TYPE  },
            BrightnessLevel: brightness
        }),
        contentType: 'application/json',
        success: function (result) {
            $("div.editing-image").html(result);
            BRIGHTNESS_LEVEL = brightness;
            allowBrightnessOrNot();
        },
        error: function (xhr) {
            alert(xhr.responseText);
        }
    });
}

function changeContrast() {
    let contrast = $("#contrast input").val();
    $.ajax({
        url: "/api/images/transformations/contrast",
        type: 'POST',
        data: JSON.stringify({
            Image: { Base64: ORIGINAL_BASE_64, ContentType: CONTENT_TYPE },
            ContrastLevel: contrast
        }),
        contentType: 'application/json',
        success: function (result) {
            $("div.editing-image").html(result);
            CONTRAST_LEVEL = Number.parseInt(contrast);
            allowContrastOrNot();
        },
        error: function (xhr) {
            alert(xhr.responseText);
        }
    });
}

function allowContrastOrNot() {
    if (ORIGINAL_BASE_64 != "") {
        let contrast = $("#contrast input").val();
        let button = document.querySelector("#contrast button");
        if (Number.parseInt(contrast) == CONTRAST_LEVEL) {
            button.style.display = 'none';
        } else {
            button.style.display = 'block';
        }
    }
}

function allowBrightnessOrNot() {
    if (ORIGINAL_BASE_64 != "") {
        let brightness = $("#brightness input").val();
        let button = document.querySelector("#brightness button");
        if (Number.parseInt(brightness) == BRIGHTNESS_LEVEL) {
            button.style.display = 'none';
        } else {
            button.style.display = 'block';
        }
    }
}

function reset() {
    let img = document.querySelector("img");
    img.src = `data:${CONTENT_TYPE};base64, ${ORIGINAL_BASE_64}`;
    $("#image-base64-hidden").val(ORIGINAL_BASE_64);
}