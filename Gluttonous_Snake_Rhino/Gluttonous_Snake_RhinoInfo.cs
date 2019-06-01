using System;
using System.Drawing;
using Grasshopper.Kernel;

namespace Gluttonous_Snake_Rhino
{
    public class Gluttonous_Snake_RhinoInfo : GH_AssemblyInfo
    {
        public override string Name
        {
            get
            {
                return "GluttonousSnakeRhino";
            }
        }
        public override Bitmap Icon
        {
            get
            {
                //Return a 24x24 pixel bitmap to represent this GHA library.
                return Resource.Icon;
            }
        }
        public override string Description
        {
            get
            {
                //Return a short string describing the purpose of this GHA library.
                return "Just for fun and practice, a gluttonous snake in rhino.";
            }
        }
        public override Guid Id
        {
            get
            {
                return new Guid("4a9a8bf1-b379-499e-b2e3-bb6f5ead59e2");
            }
        }

        public override string AuthorName
        {
            get
            {
                //Return a string identifying you or your company.
                return "";
            }
        }
        public override string AuthorContact
        {
            get
            {
                //Return a string representing your preferred contact details.
                return "";
            }
        }
    }
}
