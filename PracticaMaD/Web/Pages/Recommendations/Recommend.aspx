<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="Recommend.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Recommendations.Recommend" meta:resourcekey="Page" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation" runat="server">
    -
    <asp:Localize ID="Localize1" runat="server" meta:resourcekey="lclMenuExplanation" />
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <div id="form">
        <form id="RecommendationForm" method="post" runat="server">
            <h2><asp:Localize ID="lclRecommendEvent" runat="server" meta:resourcekey="lclRecommendEvent" /></h2>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclRecommendation" runat="server" meta:resourcekey="lclRecommendation" />
                </span>
                <span class="entry">
                    <asp:TextBox CssClass="textbox" Columns="16" ID="txtRecommendation" runat="server" Width="200px"></asp:TextBox>
                </span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclGroups" runat="server" meta:resourcekey="lclGroups" />
                </span>
                <span class="entry">
                    <asp:CheckBoxList ID="GroupList" runat="server" Width="200px"></asp:CheckBoxList>
                </span>
            </div>
            <div class="button">
                <asp:Button ID="btnRecommend" runat="server" OnClick="clickRecommend" meta:resourcekey="BtnRecommend" />
            </div>
            
        </form>
    </div>
    <br/>
    <asp:Label ID="lblRecommendOkey"  runat="server" style="color:red" meta:resourcekey="lblRecommendOkey"></asp:Label>
    <br/>
</asp:Content>