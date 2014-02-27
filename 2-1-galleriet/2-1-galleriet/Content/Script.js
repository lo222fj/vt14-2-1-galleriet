window.onload = function () {
    var webbAdress=window.location.search;
    console.log("window.location.search = " + webbAdress);
    console.log("-----");

    var aTagArray = document.getElementsByTagName("a");
    var i;
    for (i=0; i < aTagArray.length; i +=1){
        var aTagString = aTagArray[i].href;
        console.log("a-taggens href som string : " + aTagString);
        if (aTagString.indexOf(webbAdress)>-1) {
            aTagArray[i].focus();
            console.log("Plupp");

            //aTagArray[i].style.border.bold;
            //aTagArray[i].setAttribute("class", );
        }
    }

    //string.match("");
}