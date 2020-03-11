﻿/**
*    Copyright(C) G1ANT Ltd, All rights reserved
*    Solution G1ANT.Addon, Project G1ANT.Addon.MSOffice
*    www.g1ant.com
*
*    Licensed under the G1ANT license.
*    See License.txt file in the project root for full license information.
*
*/
using G1ANT.Addon.MSOffice.Api.Access;
using Microsoft.Office.Interop.Access;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace G1ANT.Addon.MSOffice.Models.Access
{
    public class AccessControlModel
    {
        public string Name { get; }
        public string Type { get; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public dynamic Caption { get; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public dynamic Value { get; }

        [JsonIgnore]
        public _Control Control { get; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<AccessControlModel> Children;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public AccessPropertiesModel Properties;


        public void SetFocus() => Control.SetFocus();

        public AccessControlModel GetParent()
        {
            if (Control.Parent is _Control)
                return new AccessControlModel(Control.Parent, false, false);

            return null;
        }

        public List<ItemDataModel> GetItemsSelected() => new SelectedItemDataCollectionModel(this);
        public ItemDataCollectionModel GetItems() => new ItemDataCollectionModel(this);
        public bool IsItemSelected(int index) => Control.Selected[index] != 0;
        public void SetItemSelected(int index, bool selected) => Control.Selected[index] = selected ? 1 : 0;


        public AccessControlModel(_Control control, bool getProperties = true, bool getChildrenRecursively = true)
        {
            Control = control ?? throw new ArgumentNullException(nameof(control));
            Name = control.Name;
            Type = ((AcControlType)control.TryGetPropertyValue<int>("ControlType")).ToString();

            if (getProperties && control.Properties.Count > 0)
            {
                var properties = control.Properties.OfType<dynamic>().ToList();
                Caption = control.TryGetPropertyValue<string>("Caption");
                Value = control.TryGetPropertyValue<string>("Value");
                Properties = new AccessPropertiesModel(control.Properties);
            }

            if (getChildrenRecursively)
                LoadChildren(getProperties);
        }


        public void LoadChildren(bool getProperties = true)
        {
            try
            {
                if (Control.Controls.Count > 0)
                    Control.Controls.Cast<Control>().Select(c => new AccessControlModel(c, getProperties, true)).ToList();
            }
            catch { }
        }

        public AccessFormModel GetForm()
        {
            return Control.Form != null ? new AccessFormModel(Control.Form, false, false, false) : null;
        }


    }
}
