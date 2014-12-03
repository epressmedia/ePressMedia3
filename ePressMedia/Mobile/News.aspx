<%@ Page Language="C#" AutoEventWireup="true" Inherits="Mobile_News" Codebehind="News.aspx.cs" %>
<html>
<head>
    <title>씨애틀 교차로</title>

    <meta name="viewport"
        content="width=device-width,minimum-scale=1.0,maximum-scale=1.0" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
        <link rel="apple-touch-icon-precomposed" href="images/iphone_icon.png" />
    <script type="text/javascript" src="Scripts/bookmark_bubble.js"></script>
    <script type="text/javascript" src="Scripts/knn_bookmark_bubble.js"></script>

    <link rel="Stylesheet" href="Styles/knn-mobile.css" />
    <link rel="Stylesheet" href="Styles/jquery.mobile-1.0a4.1.min.css" />

    <script type="text/javascript" src="Scripts/jquery-1.6.1.min.js"></script>
    <script type="text/javascript" src="Scripts/jquery.mobile-1.0a4.1.min.js"></script>
    <script type="text/javascript" src="Scripts/knn_mobile.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div data-role="page">
        <div data-role="header" data-theme="c">
            <div id = "top">
            <div id="logodiv">
            <img id="logo" src="images/m_logo.png" alt="" />
            </div>
            <div id="weatherdiv">
            <asp:Label ID="weatherlbl" runat="server" />
            <asp:Image ID="weatherImg" runat="server" />
            </div>
            <div class="clear"></div>
            </div>
            <hr id="logobreaker" />
            <div data-role="navbar">
                <ul>
                    <li tag="classified"><a>생활정보</a></li>
                    <li tag="news"><a class="ui-btn-active">뉴스</a></li>
                    <li tag="biz"><a>업소록</a></li>
                </ul>
            </div>
            <!-- /navbar -->
        </div>
        <!-- /header -->
        <div data-role="content">
            <asp:PlaceHolder ID="placeholder1" runat="server"></asp:PlaceHolder>
                                    <div class="bottom_ad">
                <img id="bottom_ad" src="images/bottomad.gif" alt="" />
                </div>
        </div>

        <!-- /content -->
        <div data-role="footer" data-theme="d">
            <p>
            </p>
            <div id="footerlinks">
                <a href="#">PC버전</a> <a>|</a> <a href="#">이용약관</a> <a>|</a> <a href="#">Contact Us</a>
            </div>
            <div id="footsign">
                <a href="http://www.future-it.ca" target="_blank">Powered by <b>Future I.T.</b></a>
            </div>

        </div>
        <!-- /footer -->


    </div>
    <!-- /page -->
    </form>
</body>
</html>
