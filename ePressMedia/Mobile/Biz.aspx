<%@ Page Language="C#" AutoEventWireup="true" Inherits="Mobile_Biz" Codebehind="Biz.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html> 
	<head> 
	<title>씨애틀 교차로</title> 
    <link rel="Stylesheet"" href = "Styles/knn-mobile.css" />
    <link rel="Stylesheet"" href = "Styles/jquery.mobile-1.0a4.1.min.css" />


	<script type="text/javascript" src="Scripts/jquery-1.6.1.min.js"></script>
    <script type="text/javascript" src="Scripts/jquery.mobile-1.0a4.1.min.js"></script>
    <script type="text/javascript" src="Scripts/knn_mobile.js"></script>
    
</head> 
<body>
    
<div data-role="page" id = "biz_default">

	<div data-role="header" data-theme="c">
 
	           
     <img id="logo" src="images/m_logo.png" alt="" />
    <hr id="logobreaker" />
    
<%--               <div data-role="navbar" >
		    <ul>
                <li tag="classified"><a>생활정보</a></li>
			    <li tag="news"><a>뉴스</a></li>
                <li tag="biz"><a class="ui-btn-active">업소록</a></li>
		    </ul>
	    </div><!-- /navbar -->--%>
        
	</div><!-- /header -->





  
	<div data-role="content">	

<div data-role="fieldcontain">
    <asp:PlaceHolder ID = "placeholder1" runat ="server"></asp:PlaceHolder>
                </div>
                <div class="bottom_ad">
                <img id="bottom_ad" src="images/bottomad.gif" alt="" />
                </div>
	</div><!-- /content -->

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

      
   </div><!-- /page -->


   <div id="map_canvas" data-role="page" data-theme="b" data-fullscreen="true"></div>
   <!--second page for showing map-->



</body>
</html>
