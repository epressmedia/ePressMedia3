<%@ Page Language="C#" AutoEventWireup="true" Inherits="Mobile_ClassifiedDetail" Codebehind="ClassifiedDetail.aspx.cs" %>



<html>
<head>
    <title>씨애틀 교차로</title>
     <link rel="Stylesheet"" href = "Styles/knn-mobile.css" />
    <link rel="Stylesheet"" href = "Styles/jquery.mobile-1.0a4.1.min.css" />
    
    
	    <script type="text/javascript" src="Scripts/jquery-1.6.1.min.js"></script>
    <script type="text/javascript" src="Scripts/jquery.mobile-1.0a4.1.min.js"></script>
    <script type="text/javascript" src="Scripts/knn_mobile.js"></script> 
    <script type="text/javascript" src="Scripts/jquery.touch-gallery-1.0.0.min.js"></script> 
    
    <script type="text/javascript">
        $(document).ready(function () {
               $('img[data-large]').touchGallery({
           getSource: function () {
               return $(this).attr('data-large');
           }
       });
   });
   
    </script>
    
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
                    <li tag="default"><a data-icon="grid">뉴스</a></li>
                    <li tag="classified"><a class="ui-btn-active"data-icon="star">사고/팔고</a></li>
                    <li tag="biz"><a data-icon="home">업소록</a></li>
                </ul>
            </div>
            <!-- /navbar -->

            <asp:Label ID="headerlable" runat="server" Visible=false/>
        </div>



        <div data-role="content" data-theme= "b">
        <asp:PlaceHolder ID = "placeholder1" runat = "server"></asp:PlaceHolder>
        </div>

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
    </form>
</body>
</html>
