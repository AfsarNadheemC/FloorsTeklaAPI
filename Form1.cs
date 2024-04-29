using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TSM = Tekla.Structures.Model;
using TSG = Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using Render;

namespace FloorsTeklaAPI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        static int Swidth;
        static int Slength;
     

        private void Generate_Click(object sender, EventArgs e)
        {
            TSM.Model model = new TSM.Model();
            if (model.GetConnectionStatus() )
            {
                int floor;
                int width;
                int length;

                if (txtFloor.Text.Length > 0)
                {
                    floor = Convert.ToInt16(txtFloor.Text);
                }
                else
                {
                    floor = 4;
                }

                if (txtLength.Text.Length > 0)
                {
                    length = Convert.ToInt32(txtLength.Text);
                }

                else
                {
                     length = 20000;
                }

                if (txtWidth.Text.Length > 0)
                {
                    width = Convert.ToInt16(txtWidth.Text);
                }

                else 
                {
                    width = 10000;
                }

                Swidth = width;
                Slength = length;

                int height = 0;

                for (int i = 0; i < floor; i++)
                {

                    TSG.Point point = new TSG.Point(0,0,height);
                    TSG.Point point1 = new TSG.Point(0,0,height + 5000);

                    TSM.Beam beam = new TSM.Beam(point,point1);

                    beam.Profile.ProfileString = "ISMB300";
                    beam.Material.MaterialString = "IS2062";
                    beam.Class = "7";

                    beam.Insert();

                    // SubBeam
                    subBeam(width, 0, height, true, false);

                    TSG.Point point2 = new TSG.Point(width, 0, height);
                    TSG.Point point3 = new TSG.Point(width, 0, height+5000);

                    TSM.Beam beam1 = new TSM.Beam(point2, point3);

                    beam1.Profile.ProfileString = "ISMB400";
                    beam1.Material.MaterialString = "IS2062";
                    beam1.Class = "7";

                    beam1.Insert();

                    // SubBeam
                    subBeam (width, length, height, false, false);

                    TSG.Point point4 = new TSG.Point(0, length, height);
                    TSG.Point point5 = new TSG.Point(0, length, height+5000);

                    TSM.Beam beam2 = new TSM.Beam(point4, point5);

                    beam2.Profile.ProfileString = "ISMB400";
                    beam2.Material.MaterialString = "IS2062";
                    beam2.Class = "7";

                    beam2.Insert();

                    // SubBeam
                    subBeam(width, length, height, true, false);

                    TSG.Point point6 = new TSG.Point(width, length, height);
                    TSG.Point point7 = new TSG.Point(width, length, height + 5000);

                    TSM.Beam beam3 = new TSM.Beam(point6, point7);

                    beam3.Profile.ProfileString = "ISMB400";
                    beam3.Material.MaterialString = "IS2062";
                    beam3.Class = "7";

                    beam3.Insert();

                    // SubBeam
                    subBeam(0, length, height, false, true);

                    int stepCount = 10 ;
                    int[,] stepsPoint = new int[ stepCount-1 , 3];

                    for (int j=0; j<stepCount-1; j++)
                    {
                        stepsPoint[j, 0] = width;
                        stepsPoint[j, 1] = ((j + 5) * 500 );
                        stepsPoint[j , 2] = (j+1)*500 + height;

                        ContourPoint contourPoint4 = new ContourPoint(new TSG.Point(stepsPoint[j, 0], stepsPoint[j, 1], stepsPoint[j,2]), null);
                        ContourPoint contourPoint5 = new ContourPoint(new TSG.Point(stepsPoint[j, 0] - 5000, stepsPoint[j, 1], stepsPoint[j, 2]), null);
                        ContourPoint contourPoint6 = new ContourPoint(new TSG.Point(stepsPoint[j, 0] - 5000, stepsPoint[j, 1] + 500, stepsPoint[j, 2]), null);
                        ContourPoint contourPoint7 = new ContourPoint(new TSG.Point(stepsPoint[j, 0], stepsPoint[j, 1] + 500, stepsPoint[j, 2]), null);

                        TSM.ContourPlate contourPlate2 = new TSM.ContourPlate();

                        contourPlate2.AddContourPoint(contourPoint4);
                        contourPlate2.AddContourPoint(contourPoint5);
                        contourPlate2.AddContourPoint(contourPoint6);
                        contourPlate2.AddContourPoint(contourPoint7);

                        contourPlate2.Profile.ProfileString = "PLT10";
                        contourPlate2.Material.MaterialString = "IS2062";
                        contourPlate2.Class = "4";

                        contourPlate2.Insert();

                    }

                    ContourPoint contourPoint = new ContourPoint(new TSG.Point (0,0,height+5000), null);
                    ContourPoint contourPoint1a = new ContourPoint(new TSG.Point(width , 0, height + 5000), null);
                    ContourPoint contourPoint1b = new ContourPoint(new TSG.Point(width , 2500, height + 5000), null);
                    ContourPoint contourPoint1c = new ContourPoint(new TSG.Point(width-5000 , 2500, height + 5000), null);
                    ContourPoint contourPoint2 = new ContourPoint(new TSG.Point(width-5000, 7000, height + 5000), null);
                    ContourPoint contourPoint3 = new ContourPoint(new TSG.Point(width, 7000, height + 5000), null);
                    ContourPoint contourPoint3a = new ContourPoint(new TSG.Point(width , length, height + 5000), null);
                    ContourPoint contourPoint3b = new ContourPoint(new TSG.Point(0, length, height + 5000), null);


                    TSM.ContourPlate contourPlate = new TSM.ContourPlate();

                    contourPlate.AddContourPoint(contourPoint);
                    contourPlate.AddContourPoint(contourPoint1a);
                    contourPlate.AddContourPoint(contourPoint1b);
                    contourPlate.AddContourPoint(contourPoint1c);
                    contourPlate.AddContourPoint (contourPoint2);
                    contourPlate.AddContourPoint(contourPoint3);
                    contourPlate.AddContourPoint(contourPoint3a);
                    contourPlate.AddContourPoint(contourPoint3b);

                    contourPlate.Profile.ProfileString = "PLT10";
                    contourPlate.Material.MaterialString = "IS2062";
                    contourPlate.Class = "4";

                    contourPlate.Insert();


                        ContourPoint contourPoint8 = new ContourPoint(new TSG.Point(0, 0, height + 5000), null);
                        ContourPoint contourPoint9 = new ContourPoint(new TSG.Point(0, 0, height + 8000), null);
                        ContourPoint contourPoint10 = new ContourPoint(new TSG.Point(width, 0, height + 8000), null);
                        ContourPoint contourPoint11 = new ContourPoint(new TSG.Point(width, 0, height + 5000), null);


                        TSM.ContourPlate contourPlate3 = new TSM.ContourPlate();

                        contourPlate3.AddContourPoint(contourPoint8);
                        contourPlate3.AddContourPoint(contourPoint9);
                        contourPlate3.AddContourPoint(contourPoint10);
                        contourPlate3.AddContourPoint(contourPoint11);

                        contourPlate3.Profile.ProfileString = "PLT10";
                        contourPlate3.Material.MaterialString = "IS2062";
                        contourPlate3.Class = "4";

                        contourPlate3.Insert();

                        ContourPoint contourPoint12 = new ContourPoint(new TSG.Point(0, 0, height + 5000), null);
                        ContourPoint contourPoint13 = new ContourPoint(new TSG.Point(0, 0, height + 8000), null);
                        ContourPoint contourPoint14 = new ContourPoint(new TSG.Point(0, length, height + 8000), null);
                        ContourPoint contourPoint15 = new ContourPoint(new TSG.Point(0, length, height + 5000), null);


                        TSM.ContourPlate contourPlate4 = new TSM.ContourPlate();

                        contourPlate4.AddContourPoint(contourPoint12);
                        contourPlate4.AddContourPoint(contourPoint13);
                        contourPlate4.AddContourPoint(contourPoint14);
                        contourPlate4.AddContourPoint(contourPoint15);

                        contourPlate4.Profile.ProfileString = "PLT10";
                        contourPlate4.Material.MaterialString = "IS2062";
                        contourPlate4.Class = "4";

                        contourPlate4.Insert();

                        ContourPoint contourPoint16 = new ContourPoint(new TSG.Point(width, length, height + 5000), null);
                        ContourPoint contourPoint17 = new ContourPoint(new TSG.Point(width, length, height + 8000), null);
                        ContourPoint contourPoint18 = new ContourPoint(new TSG.Point(width, 0, height + 8000), null);
                        ContourPoint contourPoint19 = new ContourPoint(new TSG.Point(width, 0, height + 5000), null);


                        TSM.ContourPlate contourPlate5 = new TSM.ContourPlate();

                        contourPlate5.AddContourPoint(contourPoint16);
                        contourPlate5.AddContourPoint(contourPoint17);
                        contourPlate5.AddContourPoint(contourPoint18);
                        contourPlate5.AddContourPoint(contourPoint19);

                        contourPlate5.Profile.ProfileString = "PLT10";
                        contourPlate5.Material.MaterialString = "IS2062";
                        contourPlate5.Class = "4";

                        contourPlate5.Insert();


                        ContourPoint contourPoint20 = new ContourPoint(new TSG.Point(width, length, height + 5000), null);
                        ContourPoint contourPoint21 = new ContourPoint(new TSG.Point(width, length, height + 8000), null);
                        ContourPoint contourPoint22 = new ContourPoint(new TSG.Point(0, length, height + 8000), null);
                        ContourPoint contourPoint23 = new ContourPoint(new TSG.Point(0, length, height + 5000), null);


                        TSM.ContourPlate contourPlate6 = new TSM.ContourPlate();

                        contourPlate6.AddContourPoint(contourPoint20);
                        contourPlate6.AddContourPoint(contourPoint21);
                        contourPlate6.AddContourPoint(contourPoint22);
                        contourPlate6.AddContourPoint(contourPoint23);

                        contourPlate6.Profile.ProfileString = "PLT10";
                        contourPlate6.Material.MaterialString = "IS2062";
                        contourPlate6.Class = "4";

                        contourPlate6.Insert();

              

                    model.CommitChanges();

                    height += 5000;
                }
                






            }
        }

        public static void subBeam (int width, int length, int height, bool xx, bool cross)
        {

            int limit;
            int bb;
            int w1;
            int w2;
            int l1;
            int l2;

            if (xx)
            {
                 l1 = length;
                 l2 = length;
                 w1 = 0;
                w2 = Swidth;
            }
            else
            {
                w1 = width;
                w2 = width;
                l1 = 0; 
                l2 = Slength;
            }

            TSG.Point point4a = new TSG.Point(w1, l1 , height + 5000);
            TSG.Point point5a = new TSG.Point(w2, l2 , height + 5000);

            TSM.Beam beamax = new TSM.Beam(point4a, point5a);

            beamax.Profile.ProfileString = "ISMB300";
            beamax.Material.MaterialString = "IS2062";
            beamax.Class = "12";

            beamax.Insert();


            if (xx)
            {
                limit = width / 4000;
                bb = width % 4000;
              //  MessageBox.Show("width : " + Convert.ToString(width));
            }

            else
            {
                limit = length / 4000;
                bb = length % 4000;
            }

            bb /= limit;

            if (cross && length < 16000)
            {
                cross = false;
                TSG.Point pointa = new TSG.Point(0, length/2, height+5000);
                TSG.Point point1a = new TSG.Point(Swidth, length/2, height + 5000);

                TSM.Beam beama = new TSM.Beam(pointa, point1a);

                beama.Profile.ProfileString = "ISMB300";
                beama.Material.MaterialString = "IS2062";
                beama.Class = "12";

                beama.Insert();
            }

            for (int j = 1; j < limit ; j++)
            {
                if ( xx )
                {
                    width = j * 4000 + bb;
                }

                else
                {
                    length = j * 4000 + bb;
                }

               // MessageBox.Show("width : "+Convert.ToString(width) +" bb : "+Convert.ToString(bb) + " length : "+ Convert.ToString(length));

                TSG.Point pointa = new TSG.Point(width, length, height);
                TSG.Point point1a = new TSG.Point(width, length, height + 5000);

                TSM.Beam beama = new TSM.Beam(pointa, point1a);

                beama.Profile.ProfileString = "ISMB300";
                beama.Material.MaterialString = "IS2062";
                beama.Class = "12";

                beama.Insert();

                if (cross) { 

                    if (j <= limit / 2 && j%2 == 0 || j > limit/2 && (limit-j) %2 == 0){
                        TSG.Point point2a = new TSG.Point(0, j * 4000 + bb, height + 5000);
                        TSG.Point point3a = new TSG.Point(Swidth, j * 4000 + bb, height + 5000);

                        TSM.Beam beam1a = new TSM.Beam(point2a, point3a);

                        beam1a.Profile.ProfileString = "ISMB300";
                        beam1a.Material.MaterialString = "IS2062";
                        beam1a.Class = "12";

                        beam1a.Insert();
                    }
                
                }
            }
        }

        private void txtWidth_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
