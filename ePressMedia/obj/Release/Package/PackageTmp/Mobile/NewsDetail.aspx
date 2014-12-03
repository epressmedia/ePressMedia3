<%@ Page Language="C#" AutoEventWireup="true" Inherits="Mobile_NewsDetail" Codebehind="NewsDetail.aspx.cs" %>



<html>
<head>
    <title>씨애틀 교차로 뉴스</title>
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

            <div class="clear"></div>
            </div>
            <hr id="logobreaker" />
            <div data-role="navbar">
                <ul>
                    <li><a class="ui-btn-active" data-icon="grid">뉴스</a></li>
                    <li tag="classified"><a data-icon="star">사고/팔고</a></li>
                    <li tag="biz"><a data-icon="home">업소록</a></li>
                </ul>
            </div>
            <!-- /navbar -->
        </div>
        
        <div data-role="content" data-theme= "c" >
        <asp:PlaceHolder ID = "placeholder1" runat = "server"></asp:PlaceHolder>
        </div>

        <div data-role="footer" data-theme="c">
        
        </div>
    </div>
    
    </form>
</body>
</html>
