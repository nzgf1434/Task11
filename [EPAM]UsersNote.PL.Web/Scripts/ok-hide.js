document.onclick = function (e) {
    var block1,
        parentBlock1;
    if (e.target.classList[0] === "giveawdOk") {
        parentBlock1 = e.target.parentNode;
        block1 = parentBlock1.getElementsByClassName("awards-pad");
        block1[0].classList.remove("apear");
    }
}