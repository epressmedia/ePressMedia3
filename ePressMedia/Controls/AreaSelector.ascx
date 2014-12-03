<%@ Control Language="C#" AutoEventWireup="true" Inherits="Controls_AreaSelector" Codebehind="AreaSelector.ascx.cs" %>

<style>
    .areaSelector div
    {
    	margin: 5px 0 5px 0;
    	display: inline;
    }
</style>
<div class="areaSelector">


   
            <asp:UpdatePanel ID="up1" runat="server">
            <ContentTemplate>
            <div>
     <ajaxToolkit:ComboBox ID ="cbo_province" runat="server" AutoPostBack="true" 
            AutoCompleteMode="SuggestAppend" CaseSensitive="false" 
            onselectedindexchanged="cbo_province_SelectedIndexChanged"></ajaxToolkit:ComboBox>
     </div>
     <div>
        <ajaxToolkit:AutoCompleteExtender ID="CityAutoComplete" runat="server" TargetControlID="txtCity"
                    EnableCaching="false" CompletionSetCount="20" MinimumPrefixLength="1" ServicePath="~/WebService/GeoService.asmx"
                    FirstRowSelected="true" ServiceMethod="GetCityList"  />
                <asp:TextBox ID="txtCity" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="도시를 입력해주세요" ControlToValidate="txtCity"></asp:RequiredFieldValidator>
         <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="잘못된 도시이름입니다." ControlToValidate = "txtCity" OnServerValidate="validate_SelectedArea"></asp:CustomValidator>
         
        </div>
        </ContentTemplate>
        </asp:UpdatePanel>
        </div>