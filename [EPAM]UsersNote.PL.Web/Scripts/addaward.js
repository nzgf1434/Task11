document.onclick = function(e) {
    var block,
        inp,
        options,
        i,
        parentBlock;
        if (e.target.classList[0] === "giveawd") {
            parentBlock = e.target.parentNode;
            block = parentBlock.getElementsByClassName("awards-pad");
            block[0].classList.add("apear");
        }
        else if (e.target.classList[1] === "glyphicon-star") {
            parentBlock = e.target.parentNode.parentNode;
            block = parentBlock.getElementsByClassName("awards-pad");
            block[0].classList.add("apear");
        }
        else if (e.target.classList[0] === "giveawdOk") {
            parentBlock = e.target.parentNode;
            inp = parentBlock.getElementsByTagName("input");
            options = parentBlock.parentNode.getElementsByTagName("option");
            for (i = 0; i < options.length; i++) {
                if(options[i].selected == true){
                    inp[0].value += options[i].value + "!";
                }
            }
            block = parentBlock.getElementsByClassName("awards-pad");
            block[0].classList.remove("apear");
        }
    }