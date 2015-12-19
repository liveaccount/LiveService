<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Web Sockets in ASP.NET</title>
</head>
<body>
    <input type="button" onclick="javascript:connectToServer();" value='connect' />
    <textarea id='log' rows="25" cols="70"></textarea>
    <script type="text/javascript">
        var sock;
        function connectToServer() {
            try {
                sock = new WebSocket("ws://localhost:8181/websock");
                //sock = new WebSocket("ws://192.168.0.100:8181/websock");

                //sock = new WebSocket("ws://websockets.org:8787");

                sock.onopen = sockOpen;
                sock.onerror = sockError;
                sock.onclose = sockClosed;
                sock.onmessage = sockMessage;
            } catch (e) {
                log("error:" + e);
            }
        }

        function sockOpen() {
            log("connected");
        }

        function sockError(error, p2) {
            log("socket error!");
        }

        function sockClosed() {
            log("socket closed");
        }

        function sockMessage(event) {
            log("Received message: " + event.data);
        }

        function log(msg) {
            var txtLog = document.getElementById("log");
            txtLog.value += "\r\n" + msg;
        }
    </script>
</body>
</html>
