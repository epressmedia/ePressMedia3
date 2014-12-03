<%@ Control Language="C#" AutoEventWireup="true" Inherits="Biz_BizSearch" Codebehind="BizSearch.ascx.cs" %>
<asp:UpdatePanel runat="server">
<ContentTemplate>
       <div id="srchPnl" class="bizSearchBox">
       <p class="bizSearchName">업소 검색</p>
          <div class="srchSub1">
            업소명<asp:TextBox ID="SearchName" runat="server" CssClass="srchBox" />
          </div>
          
          <div class="srchSub2">
            업종<asp:DropDownList ID="BizCatList" runat="server" CssClass="catBox" />
          </div>
          <div class="divClear"></div>
      
        
          <div class="srchSub3">
            주(State/Province) 
            <ajaxToolkit:ComboBox ID ="cbo_province" runat="server" AutoPostBack="true" 
            AutoCompleteMode="SuggestAppend" CaseSensitive="false" 
            onselectedindexchanged="cbo_province_SelectedIndexChanged" class="state_ddl"></ajaxToolkit:ComboBox>
            </div>
           <div class="srchSub4">
            도시(City)  
            <ajaxToolkit:AutoCompleteExtender ID="CityAutoComplete" runat="server" TargetControlID="txtCity"
                    EnableCaching="false" CompletionSetCount="20" MinimumPrefixLength="1" ServicePath="~/WebService/GeoService.asmx"
                    FirstRowSelected="true" ServiceMethod="GetCityList"  />
                <asp:TextBox ID="txtCity" runat="server"></asp:TextBox>
            <div class="divClear"></div>
            <div class="srchButton">
            <asp:Button ID="SearchButton" runat="server" Text="검색" CausesValidation="false"
              onclick="SearchButton_Click" class="srchButton" />
              </div>
          </div>



          <div class="divClear"></div>
        
                      
        </div>
            </ContentTemplate>
          </asp:UpdatePanel>