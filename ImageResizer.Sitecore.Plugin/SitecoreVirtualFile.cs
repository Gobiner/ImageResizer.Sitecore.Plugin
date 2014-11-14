﻿using ImageResizer.Plugins;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageResizer.Sitecore.Plugin
{
    public class SitecoreVirtualFile : IVirtualFile
    {
        protected ResizeSettings query;

        public SitecoreVirtualFile(string virtualPath, NameValueCollection query)
        {
            this._virtualPath = virtualPath;
            this.query = new ResizeSettings(query);
        }

        public System.IO.Stream Open()
        {
            DynamicLink dynamicLink;
            if (!DynamicLink.TryParse(VirtualPath, out dynamicLink))
                throw new ApplicationException("VirtualImageProviderPlugin : cannot parse virtual path: " + VirtualPath);

            MediaItem mediaItem = Sitecore.Context.Database.GetItem(dynamicLink.ItemId, dynamicLink.Language ?? Sitecore.Context.Language);

            return mediaItem.GetMediaStream();
        }

        protected string _virtualPath;
        public string VirtualPath
        {
            get { return _virtualPath; }
        }
    }
}
