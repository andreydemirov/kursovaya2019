using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Kursach
{
    class Parking
    {

        private int parkingSpaceNum;
        private string carOwner;
        private string carModel;
        private string parkingMark;
        private string paymentMark;

        public Parking()
        {

        }

        public Parking(int parkingSpaceNum, string carOwner, string carModel, string parkingMark, string paymentPark)
        {
            this.ParkingSpaceNum = parkingSpaceNum;
            this.CarOwner = carOwner;
            this.CarModel = carModel;
            this.ParkingMark = parkingMark;
            this.PaymentMark = paymentPark;
        }

        public int ParkingSpaceNum { get => parkingSpaceNum; set => parkingSpaceNum = value; }
        public string CarOwner { get => carOwner; set => carOwner = value; }
        public string CarModel { get => carModel; set => carModel = value; }
        public string ParkingMark { get => parkingMark; set => parkingMark = value; }
        public string PaymentMark { get => paymentMark; set => paymentMark = value; }

        public void Write(BinaryWriter bw)
        {
             
            bw.Write(ParkingSpaceNum);
            bw.Write(CarOwner);
            bw.Write(CarModel);
            bw.Write(ParkingMark);
            bw.Write(PaymentMark);
            

        }

        public static Parking Read(BinaryReader br)
        {
            Parking p = new Parking();
            p.ParkingSpaceNum = br.ReadInt32();
            p.CarOwner = br.ReadString();
            p.CarModel = br.ReadString();
            p.ParkingMark = br.ReadString();
            p.PaymentMark = br.ReadString();
            
            return p;
        }
    }
}
