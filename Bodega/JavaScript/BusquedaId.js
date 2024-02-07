var busqueda = document.getElementById("busqueda");
busqueda.style.color = "#FE980F";
busqueda.style.fontSize = '22px';
busqueda.style.fontWeight = "600";
busqueda.style.fontFamily = "Times New Roman";


var bid = document.getElementById("bid")
bid.style.color = "#AE7031";
bid.style.fontSize = '14px';
bid.style.fontWeight = "600";
bid.style.fontFamily = "Verdana";

var et_label = document.querySelectorAll("label.form-label");
for (i = 0; i <= 5; ++i) {
    et_label[i].style.color = "#AE7031";
    et_label[i].style.fontSize = '14px';
    et_label[i].style.fontWeight = "600";
    et_label[i].style.fontFamily = "Verdana";
}

var Label1 = document.getElementById("ContentPlaceHolder1_Label1");
Label1.style.color = "#663300";
Labe1.style.fontSize = '14px';
Label1.style.fontWeight = "600";
Label1.style.fontFamily = "Verdana";

var Label2 = document.getElementById("ContentPlaceHolder1_Label2");
Label1.style.color = "#663300";
Labe1.style.fontSize = '14px';
Label1.style.fontWeight = "600";
Label1.style.fontFamily = "Verdana";

var Label3 = document.getElementById("ContentPlaceHolder1_Label3");
Label1.style.color = "#663300";
Labe1.style.fontSize = '14px';
Label1.style.fontWeight = "600";
Label1.style.fontFamily = "Verdana";

var Label4 = document.getElementById("ContentPlaceHolder1_Label4");
Label1.style.color = "#663300";
Labe1.style.fontSize = '14px';
Label1.style.fontWeight = "600";
Label1.style.fontFamily = "Verdana";

var Label5 = document.getElementById("ContentPlaceHolder1_Label5");
Label1.style.color = "#663300";
Labe1.style.fontSize = '14px';
Label1.style.fontWeight = "600";
Label1.style.fontFamily = "Verdana";

var txt = document.getElementById("ContentPlaceHolder1_TextBox1");
txt.focus = function () {
    txt.style.background = "#ffcc99";
}