
<%@ Page Title="Home Page" Language="C#" AutoEventWireup="True"    Inherits="ePressMedia.Mobile.Default" Codebehind="Default.aspx.cs" %>

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

        </div>
        <!-- /header -->
        <div data-role="content">
            <img id="maindoor_logo" src="images/m_logo.png" alt="" />
            <a href="list.aspx" rel="external" data-role="button" data-icon="arrow-r" data-iconpos="right" data-theme="d">생활정보</a>
           <a href="news.aspx" rel="external" data-role="button" data-icon="arrow-r" data-iconpos="right" data-theme="d">뉴스</a>
             <a href="biz.aspx" rel="external" data-role="button" data-icon="arrow-r" data-iconpos="right" data-theme="d">업소록</a>

             
             <div class="futureit_logo">
             <p>powered by</p>
                <img id="futureit" src="images/futureit_logo.png" alt="" />
            </div>

              <input type="hidden" ID ="BookmarkBubbleNumber" value=<%= System.Configuration.ConfigurationManager.AppSettings["BookmarkBubbleNumber"]%> />
            <input type="hidden" ID ="BookmarkBubbleStoageName" value=<%= System.Configuration.ConfigurationManager.AppSettings["BookmarkBubbleStoageName"]%> />
        </div>
        <!-- /content -->



    </div>
    <!-- /page -->
    </form>
</body>
</html>
