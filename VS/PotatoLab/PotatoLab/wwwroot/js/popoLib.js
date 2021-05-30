
function aaaa() {
	alert('sss');
}
function CallServerFunction(StrPriUrl, ObjPriData, CallBackFunction) {

	//var sDataType = "text";
	//if (typeof ObjPriData == 'string') {
	//	try {
	//		ObjPriData = JSON.stringify(ObjPriData)
	//		//ObjPriData = JSON.parse(str);
	//		sDataType = "json";
	//	} catch (e) {
	//	}
	//}
	//else {
	//	sDataType = "json";
	//}

	$.ajax({
		type: "post",
		url: StrPriUrl,
		contentType: "application/json; charset=utf-8",
		data: ObjPriData,
		//dataType: sDataType,	//"text" "json"  如果後面回傳是字串要用這個text 若是物件則用JSON
		//dataType: "text",	//"text" "json"  如果後面回傳是字串要用這個text 若是物件則用JSON  (不設定會自動)
		/*dataType:"html",     //或用 "text"*/
		async: true,//最後要return或是等等需要用到result的value的話，必須設定為false(就會變成同步執行),不然就不設定，預設是true
		success: function (result) {
			if (CallBackFunction != null && typeof CallBackFunction != 'undefined') {
				CallBackFunction(result);
			}
		},
		error: function (result) {

			alert('ajax error (CallServerFunction)!!');
			//1. 檢查 dataType，試試看 text 或 json
			alert(result.responseText);
			//window.location.href = "FrmError.aspx?Exception=" + result.responseText;
		},
		async: true
	});
}


function find() {

	//CallServerFunction("Default.aspx/getHello", JSON.stringify({ str: text }), function (myresult) {
	CallServerFunction("AjaxTestPage1.aspx/getHello", "{ 'str': '123' }", function (myresult) {
		alert(JSON.stringify(myresult));
		alert(myresult.d);
	});

}

//https://dotblogs.com.tw/kevinya/2014/06/06/145405
//https://stackoverflow.com/questions/7770679/jquery-ajax-call-to-an-asp-net-webmethod
//http://atic-tw.blogspot.com/2013/05/jquery-ajax-aspnet.html


function find2() {

	//CallServerFunction("Default.aspx/getHello", JSON.stringify({ str: text }), function (myresult) {
	CallServerFunction("AjaxTestPage1.aspx/getHello2", null, function (myresult) {
		alert(JSON.stringify(myresult));
		alert(myresult.d);
	});

}
