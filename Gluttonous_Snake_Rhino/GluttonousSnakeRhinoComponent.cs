using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Grasshopper.Kernel.Attributes;
using Grasshopper.GUI;
using Grasshopper.GUI.Canvas;
using System.Drawing;
using System.Diagnostics;

// In order to load the result of this wizard, you will also need to
// add the output bin/ folder of this project to the list of loaded
// folder in Grasshopper.
// You can use the _GrasshopperDeveloperSettings Rhino command for that.

namespace Gluttonous_Snake_Rhino
{
    public class GluttonousSnakeRhinoComponent : GH_Component
    {
        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>

        public Direction NowDirection;


        //Store the body Sphere of snake.
        private List<Sphere> Bodys;

        //The food of snake.

        private Box Rabbit;


        //The time watch.

        private Stopwatch StopWatch = null;


        //For set the interval.
        private double TimeTicket;

        //
        private double TimeInterval;

        private double Speed;



        private List<Line> Guides = new List<Line>();





        public GluttonousSnakeRhinoComponent()
          : base("Gluttonous_Snake_Rhino", "Gluttonous snake.",
              "Just for fun and practice.",
              "Rabbit", "Snake")
        {
            //Initial the essential properties.
            NowDirection = Direction.Forward;
            Bodys = new List<Sphere>();


            TimeInterval = 300;
            Speed = 20;


            Rabbit = new Box(Plane.WorldXY, new Interval(-5, 5),new Interval(-5, 5),new Interval(-5, 5));

        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {

           // pManager.AddNumberParameter("Radius", "R", "The radius of snake body.", GH_ParamAccess.item);

           // pManager.AddNumberParameter("Speed", "S", "The speed of moving.", GH_ParamAccess.item);

        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {


            //Set the output parameters, body and food.
            pManager.AddGeometryParameter("Snake", "S", "The body of the snake.", GH_ParamAccess.list);


            pManager.AddGeometryParameter("Rabbit", "R", "The food you are looking for.", GH_ParamAccess.item);


            //pManager.AddLineParameter("Guides", "L", "The lines help you find the rabbit.", GH_ParamAccess.list);

        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object can be used to retrieve data from input parameters and 
        /// to store data in output parameters.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
    



            //If the begin, get first one head,
            if (Bodys.Count == 0) {

                

                Bodys.Add(new Sphere(new Point3d(0,0,0),10));
                Bodys.Add(new Sphere(new Point3d(0,0,0),10));
                


            }


            if (StopWatch == null) {
                StopWatch = new Stopwatch();

                StopWatch.Start();

                TimeTicket = StopWatch.ElapsedMilliseconds;
            }



            //this.AddRuntimeMessage(GH_RuntimeMessageLevel.Warning, StopWatch.ElapsedMilliseconds);

            if(StopWatch.ElapsedMilliseconds  - TimeTicket> TimeInterval)
            {


               TimeTicket = StopWatch.ElapsedMilliseconds;



            //If the distance between head and food is less than 10.
                if (Rabbit.Center.DistanceTo(Bodys[0].Center) <= 10) {
               Random random  = new Random();

                //For orient next food position.
                Point3d nextCenterPoint = new Point3d(
                    random.Next(0,100),
                    random.Next(0,100),
                    random.Next(0,100)
                    );
       

                Rabbit.Transform(Transform.Translation(new Vector3d(nextCenterPoint-Rabbit.Center)));



                //Add the tail

                Sphere nowTail = Bodys[Bodys.Count - 1];

                Sphere newTail = new Sphere(nowTail.Center,nowTail.Radius);



                Bodys.Add(newTail);


                

            }

            //Move the body.


  


            int temp = StopWatch.Elapsed.Milliseconds;

            this.AddRuntimeMessage(GH_RuntimeMessageLevel.Warning, temp.ToString());


            Vector3d moveVector = new Vector3d();

            double speed = TimeInterval/1000*Speed;

            switch (NowDirection) {
                case (Direction.Up):
                    moveVector = new Vector3d(0, 0, speed);
                    break;
                case (Direction.Down):
                    moveVector = new Vector3d(0, 0, -speed);
                    break;
                case (Direction.Left):
                    moveVector = new Vector3d(-speed, 0, 0);
                    break;
                case (Direction.Right):
                    moveVector = new Vector3d(speed, 0, 0);
                    break;
                case (Direction.Forward):
                    moveVector = new Vector3d(0, speed, 0);
                    break;
                case (Direction.Back):
                    moveVector = new Vector3d(0, -speed, 0);
                    break;
            }


                //moveVector = new Vector3d(100, 0, 0);


                //Remove the last body.
                //And insert the new head into the list.

                Bodys.Remove(Bodys[Bodys.Count - 1]);

                //Bodys.Insert(0, new Sphere(Bodys[0].Center, Bodys[0].Radius));


                // Move the head.


                Sphere newHead = new Sphere(Bodys[0].Center, Bodys[0].Radius);

                newHead.Translate(moveVector);


                Bodys.Insert(0,newHead);



                //Bodys[0].Translate(moveVector);


                Guides.Clear();

                foreach (Sphere body in Bodys)
                {
                    Guides.Add(new Line(body.Center, Rabbit.Center));
                }



            }



                DA.SetDataList(0, Bodys);

                DA.SetData(1, Rabbit);

               // DA.SetDataList(2, Guides);


            //Run this method again and agagin.
            ExpireSolution(true);

            

        }

        /// <summary>
        /// Provides an Icon for every component that will be visible in the User Interface.
        /// Icons need to be 24x24 pixels.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                // You can add image files to your project resources and access them like this:
                //return Resources.IconForThisComponent;
                return Resource.Icon;
            }
        }



        public override void CreateAttributes()
        {
            m_attributes = new AttributeA(this);
            //base.CreateAttributes();
        }


        private class AttributeA : GH_ComponentAttributes {


            private bool ControllerFormOpened = false;


            public AttributeA(GH_Component component) : base(component) {}


            public override void ExpireLayout()
            {
                base.ExpireLayout();
            }


            protected override void Layout()
            {

//                Bounds = new RectangleF(Pivot,new SizeF(200,200));
                base.Layout();
            }


            //When user double click the component show the control form.
            public override GH_ObjectResponse RespondToMouseDoubleClick(GH_Canvas sender, GH_CanvasMouseEvent e)
            {

                if (ControllerFormOpened == false)
                {
                    ControlForm controlForm = new ControlForm((GluttonousSnakeRhinoComponent)Owner);
                    controlForm.Show();

                    ControllerFormOpened = true;
                }







                return GH_ObjectResponse.Handled;

            }



            protected override void Render(GH_Canvas canvas, Graphics graphics, GH_CanvasChannel channel)
            {

               // GH_Capsule capsule = GH_Capsule.CreateCapsule(Bounds,GH_Palette.Pink);

                //capsule.Render(graphics, Selected, Owner.Locked, true);

                //capsule.Dispose();

                //capsule = null;
                
                base.Render(canvas, graphics, channel);
            }



        }
        /// <summary>
        /// Each component must have a unique Guid to identify it. 
        /// It is vital this Guid doesn't change otherwise old ghx files 
        /// that use the old ID will partially fail during loading.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("ec566f89-59db-4d27-bed8-a38fa38abfae"); }
        }
    }



    public enum Direction
    {

        Up,
        Down,
        Forward,
        Back,
        Left,
        Right
    }


}
