document.onclick = function (e) {
    var block,
        parentBlock;
    if (e.target.classList[0] === "button-dialog") {
        parentBlock = e.target.parentNode.parentNode;
        block = parentBlock.getElementsByClassName("div-dialog");
        block[0].classList.remove("hide");

    }
    else if (e.target.classList[0] === "close1") {
        block = e.target.parentNode.parentNode;
        block.classList.add("hide");
    }
    else if (e.target.classList[0] === "close2") {
        block = e.target.parentNode;
        block.classList.add("hide");
    }
}