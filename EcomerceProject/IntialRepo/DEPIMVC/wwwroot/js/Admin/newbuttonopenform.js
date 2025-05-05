function trydropdown(...parameters) {
    let dropDownButtons = document.querySelectorAll(`${parameters[0]}`);
    let displayedItems = document.querySelectorAll(`${parameters[1]}`);
    displayedItems.forEach((displayedItem) => {
        displayedItem.style.transform = "translateY(-10%)";
        displayedItem.style.zIndex = "-1";

    })

    dropDownButtons.forEach((dropDownButton, index) => {
        dropDownButton.addEventListener("click", function () {
            if (displayedItems[index].style.transform == "translateY(-10%)") {
                displayedItems[index].style.transform = "translateY(0%)";
                displayedItems[index].style.zIndex = "1";
                displayedItems[index].style.opacity = "1";
                displayedItems[index].style.position = "relative";
            }
            else {
                displayedItems[index].style.opacity = "0";
                displayedItems[index].style.zIndex = "-1";
                displayedItems[index].style.transform = "translateY(-10%)";
                displayedItems[index].style.position = "absolute";
            }
        })
    })


}

trydropdown(".new .input-new", ".new-catigory-form");

