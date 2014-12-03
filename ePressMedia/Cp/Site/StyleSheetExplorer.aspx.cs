using System;
using System.Collections;
using System.Data;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using System.Collections.Generic;
using Telerik.Web.UI.Widgets;
using System.Linq;



namespace ePressMedia.Cp.Site
{
    public partial class StyleSheetExplorer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Set the same skin as the one set by the SkinChooser in the Default.aspx page.
            if (!Object.Equals(Session["SkinChooserSkin"], null)) FileExplorer1.Skin = (string)Session["SkinChooserSkin"];

            string ProviderTypeName = typeof(CustomFileBrowserProviderWithFilter).AssemblyQualifiedName;
            FileExplorer1.Configuration.ContentProviderTypeName = ProviderTypeName;
        }
    }
    public class CustomFileBrowserProviderWithFilter : FileSystemContentProvider
    {
        public CustomFileBrowserProviderWithFilter(HttpContext context, string[] searchPatterns, string[] viewPaths, string[] uploadPaths, string[] deletePaths, string selectedUrl, string selectedItemTag)
            : base(context, searchPatterns, viewPaths, uploadPaths, deletePaths, selectedUrl, selectedItemTag)
        {
        }

        public override DirectoryItem ResolveDirectory(string path)
        {
            DirectoryItem originalFolder = base.ResolveDirectory(path);
            FileItem[] originalFiles = originalFolder.Files;
            List<FileItem> filteredFiles = new List<FileItem>();

            // Filter the files
            foreach (FileItem originalFile in originalFiles)
            {
                if (!this.IsFiltered(originalFile.Name))
                {
                    filteredFiles.Add(originalFile);
                }
            }

            DirectoryItem newFolder = new DirectoryItem(originalFolder.Name, originalFolder.Location, originalFolder.FullPath, originalFolder.Tag, originalFolder.Permissions, filteredFiles.ToArray(), originalFolder.Directories);

            return newFolder;
        }

        public override DirectoryItem ResolveRootDirectoryAsTree(string path)
        {
            DirectoryItem originalFolder = base.ResolveRootDirectoryAsTree(path);
            DirectoryItem[] originalDirectories = originalFolder.Directories;
            List<DirectoryItem> filteredDirectories = new List<DirectoryItem>();

            // Filter the folders 
            foreach (DirectoryItem originalDir in originalDirectories)
            {
                if (!this.IsFiltered(originalDir.Name))
                {
                    filteredDirectories.Add(originalDir);
                }
            }
            DirectoryItem newFolder = new DirectoryItem(originalFolder.Name, originalFolder.Location, originalFolder.FullPath, originalFolder.Tag, originalFolder.Permissions, originalFolder.Files, filteredDirectories.ToArray());

            return newFolder;
        }

        private bool IsFiltered(string name)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();

            if (context.StyleSheets.Count(c=>c.SystemFg == true && c.Name.ToLower().Contains(name.ToLower())) > 0)
            {
                return true;
            }

            // else
            return false;
        }
    }

}