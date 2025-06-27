<%@ Page Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" Codebehind="EventsFounded.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Event.EventsFounded"
    meta:resourcekey="Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">

    <br />
    <form runat="server">
    <p><asp:Label ID="lblNoEvents" runat="server" meta:resourcekey="lblNoEvents"></asp:Label></p>
    <asp:GridView ID="gvEvents" runat="server" CssClass="Grid" GridLines="None"
        AutoGenerateColumns="False" HorizontalAlign="Center" ShowHeaderWhenEmpty="True" OnRowCommand="gvEvents_RowCommand">
        <Columns>
            <asp:BoundField DataField="EventName" HeaderText="<%$ Resources: eventName %>" meta:resourcekey="BoundFieldResource5" />
            <asp:BoundField DataField="categoryName" HeaderText="<%$ Resources: categoryName %>" meta:resourcekey="BoundFieldResource6" />
            <asp:BoundField DataField="eventDate" HeaderText="<%$ Resources: date %>" meta:resourcekey="BoundFieldResource7" />
            
            <asp:HyperLinkField 
                DataNavigateUrlFields="EventId"
                DataNavigateUrlFormatString="~/Pages/Comment/AddComment.aspx?eventId={0}" meta:resourcekey="addcomment"/>

            <asp:TemplateField> 
                <ItemTemplate> 
                    <asp:LinkButton ID="lbVerComentarios" runat="server" CommandName="comentarios" CommandArgument='<%# Eval("eventId") %>' meta:resourcekey="btnComentarios" />
                </ItemTemplate> 
            </asp:TemplateField> 
            <asp:HyperLinkField 
                DataNavigateUrlFields="EventId"
                DataNavigateUrlFormatString="~/Pages/Recommendations/Recommend.aspx?eventId={0}"  meta:resourcekey="recommend"/>
        </Columns>
    </asp:GridView>
    </form>
    <br />
    <asp:Label ID="lblError" runat="server" ForeColor="Red" Visible="false" meta:resourcekey="lblError"></asp:Label>
    <!-- "Previous" and "Next" links. -->
    <div class="previousNextLinks">
        <span class="previousLink">
            <asp:HyperLink ID="lnkPrevious" runat="server"
                Visible="False" meta:resourcekey="lnkPrevious"></asp:HyperLink>
        </span><span class="nextLink">
            <asp:HyperLink ID="lnkNext" runat="server" Visible="False" meta:resourcekey="lnkNext"></asp:HyperLink>
        </span>
    </div>
    <br />
    <br />

</asp:Content>