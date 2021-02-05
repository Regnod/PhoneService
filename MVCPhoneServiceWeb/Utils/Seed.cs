using Data.Models;
using Microsoft.EntityFrameworkCore;
using Repo;
using System.Linq;

namespace MVCPhoneServiceWeb.Utils
{
    public static class Seed
    {
        public static async void PopulateDatabase(ApplicationDbContext db)
        {
            // Managements
            Management m1 = new Management() { Name = "Gerencia General" };
            Management m2 = new Management() { Name = "Gerencia de Finanzas" };
            Management m3 = new Management() { Name = "Gerencia de Ventas" };
            Management m4 = new Management() { Name = "Gerencia de Recambios" };
            Management m5 = new Management() { Name = "Gerencia de PostVenta Vehícular" };
            Management m6 = new Management() { Name = "Gerencia de Administración" };
            Management m7 = new Management() { Name = "Gerencia de PostVenta No Vehícular" };
            //
            db.Managements.Add(m1);
            db.Managements.Add(m2);
            db.Managements.Add(m3);
            db.Managements.Add(m4);
            db.Managements.Add(m5);
            db.Managements.Add(m6);
            db.Managements.Add(m7);
            db.SaveChanges();
            //
            m1 = db.Managements.FirstOrDefault(m => m.Name == m1.Name);
            m2 = db.Managements.FirstOrDefault(m => m.Name == m2.Name);
            m3 = db.Managements.FirstOrDefault(m => m.Name == m3.Name);
            m4 = db.Managements.FirstOrDefault(m => m.Name == m4.Name);
            m5 = db.Managements.FirstOrDefault(m => m.Name == m5.Name);
            m6 = db.Managements.FirstOrDefault(m => m.Name == m6.Name);
            m7 = db.Managements.FirstOrDefault(m => m.Name == m7.Name);

            //Cost Center 
            //Gerencia General
            CostCenter cc1 = new CostCenter() { Code = "00", Name = "No Assignado", ManagementId = m1.ManagementId };
            CostCenter cc2 = new CostCenter() { Code = "01", Name = "Gerencia General", ManagementId = m1.ManagementId };
            CostCenter cc3 = new CostCenter() { Code = "02", Name = "Informática", ManagementId = m1.ManagementId };
            CostCenter cc4 = new CostCenter() { Code = "26", Name = "Dpt de Operaciones para el Transporte", ManagementId = m1.ManagementId };
            //Gerencia de Finanzas
            CostCenter cc15 = new CostCenter() { Code = "12", Name = "Gerencia de Finanzas", ManagementId = m2.ManagementId };
            //Gerencia de Ventas 
            CostCenter cc9 = new CostCenter() { Code = "11", Name = "Aplicaciones", ManagementId = m3.ManagementId };
            CostCenter cc10 = new CostCenter() { Code = "13", Name = "Gerencia de Ventas", ManagementId = m3.ManagementId };
            CostCenter cc11 = new CostCenter() { Code = "15", Name = "Depósitos Aduanal", ManagementId = m3.ManagementId };
            CostCenter cc12 = new CostCenter() { Code = "16", Name = "Venta de Vehículos", ManagementId = m3.ManagementId };
            CostCenter cc13 = new CostCenter() { Code = "17", Name = "Venta de Motores", ManagementId = m3.ManagementId };
            CostCenter cc14 = new CostCenter() { Code = "18", Name = "Relaciones Públicas", ManagementId = m3.ManagementId };
            //Gerencia de Recambios
            CostCenter cc19 = new CostCenter() { Code = "20", Name = "Recambio Vehícular", ManagementId = m4.ManagementId };
            //Gerencia PostVenta Vehícular
            CostCenter cc20 = new CostCenter() { Code = "29", Name = "Gerencia de PostVenta Vehícular", ManagementId = m5.ManagementId };
            CostCenter cc21 = new CostCenter() { Code = "30", Name = "Taller de Coches de Turismo", ManagementId = m5.ManagementId };
            CostCenter cc22 = new CostCenter() { Code = "31", Name = "Taller de Chapistería y Pintura", ManagementId = m5.ManagementId };
            CostCenter cc23 = new CostCenter() { Code = "32", Name = "Taller de Vehículos Automotor Rosello", ManagementId = m5.ManagementId };
            CostCenter cc24 = new CostCenter() { Code = "33", Name = "Taller de Vehículos Comerciales", ManagementId = m5.ManagementId };
            CostCenter cc25 = new CostCenter() { Code = "34", Name = "Taller de Agregados", ManagementId = m5.ManagementId };
            CostCenter cc26 = new CostCenter() { Code = "35", Name = "Centro de Capacitación", ManagementId = m5.ManagementId };
            CostCenter cc27 = new CostCenter() { Code = "36", Name = "Asistencia Técnica", ManagementId = m5.ManagementId };
            CostCenter cc28 = new CostCenter() { Code = "37", Name = "Taller de Inyección", ManagementId = m5.ManagementId };
            //Gerencia de Administracion 
            CostCenter cc5 = new CostCenter() { Code = "04", Name = "Servicios Internos", ManagementId = m6.ManagementId };
            CostCenter cc6 = new CostCenter() { Code = "05", Name = "Gerencia de Administración", ManagementId = m6.ManagementId };
            CostCenter cc7 = new CostCenter() { Code = "06", Name = "Mantenimiento", ManagementId = m6.ManagementId };
            CostCenter cc8 = new CostCenter() { Code = "07", Name = "Logística", ManagementId = m6.ManagementId };
            //Gerencia de PostVenta No Vehícular
            CostCenter cc16 = new CostCenter() { Code = "14", Name = "Off-Highway", ManagementId = m7.ManagementId };
            CostCenter cc17 = new CostCenter() { Code = "19", Name = "Postventa MTU", ManagementId = m7.ManagementId };
            CostCenter cc18 = new CostCenter() { Code = "21", Name = "Recambios MTU", ManagementId = m7.ManagementId };
            //
            db.CostCenters.Add(cc1);
            db.CostCenters.Add(cc2);
            db.CostCenters.Add(cc3);
            db.CostCenters.Add(cc4);
            db.CostCenters.Add(cc5);
            db.CostCenters.Add(cc6);
            db.CostCenters.Add(cc7);
            db.CostCenters.Add(cc8);
            db.CostCenters.Add(cc9);
            db.CostCenters.Add(cc10);
            db.CostCenters.Add(cc11);
            db.CostCenters.Add(cc12);
            db.CostCenters.Add(cc13);
            db.CostCenters.Add(cc14);
            db.CostCenters.Add(cc15);
            db.CostCenters.Add(cc16);
            db.CostCenters.Add(cc17);
            db.CostCenters.Add(cc18);
            db.CostCenters.Add(cc19);
            db.CostCenters.Add(cc20);
            db.CostCenters.Add(cc21);
            db.CostCenters.Add(cc22);
            db.CostCenters.Add(cc23);
            db.CostCenters.Add(cc24);
            db.CostCenters.Add(cc25);
            db.CostCenters.Add(cc26);
            db.CostCenters.Add(cc27);
            db.CostCenters.Add(cc28);
            db.SaveChanges();

            // Calling Plans
            #region Calling Plan
            CallingPlan cp25SoloRec = new CallingPlan() { CallingPlanId = "25 Solo Rec", Minutes = 0, Cost = 25 };
            CallingPlan cpMedium = new CallingPlan() { CallingPlanId = "Medium", Minutes = 154, Cost = 45 };
            CallingPlan cpOptimo = new CallingPlan() { CallingPlanId = "Optimo", Minutes = 257, Cost = 70 };
            CallingPlan cpEconomico = new CallingPlan() { CallingPlanId = "Economico", Minutes = 385, Cost = 100 };
            CallingPlan cpPractico = new CallingPlan() { CallingPlanId = "Practico", Minutes = 514, Cost = 130 };
            CallingPlan cpVIP = new CallingPlan() { CallingPlanId = "VIP", Minutes = 771, Cost = 190 };
            db.CallingPlans.Add(cp25SoloRec);
            db.CallingPlans.Add(cpMedium);
            db.CallingPlans.Add(cpOptimo);
            db.CallingPlans.Add(cpEconomico);
            db.CallingPlans.Add(cpPractico);
            db.CallingPlans.Add(cpVIP);
            db.SaveChanges();

            cp25SoloRec = db.CallingPlans.FirstOrDefault(m => m.CallingPlanId == "25 Solo Rec");
            cpMedium    = db.CallingPlans.FirstOrDefault(m => m.CallingPlanId == "Medium"); 
            cpOptimo    = db.CallingPlans.FirstOrDefault(m => m.CallingPlanId == "Optimo");
            cpEconomico = db.CallingPlans.FirstOrDefault(m => m.CallingPlanId == "Economico"); 
            cpPractico  = db.CallingPlans.FirstOrDefault(m => m.CallingPlanId == "Practico"); 
            cpVIP       = db.CallingPlans.FirstOrDefault(m => m.CallingPlanId == "VIP");
            #endregion


            SMSPlan spLow = new SMSPlan() { SMSPlanId = "Low", Cost = 10, Messages = 111 };
            SMSPlan spHigh = new SMSPlan() { SMSPlanId = "High", Cost = 20, Messages = 222 };
            SMSPlan sp25SoloRec = new SMSPlan() { SMSPlanId = "25SoloRec", Cost = 25, Messages = 150 };
            SMSPlan spVIP = new SMSPlan() { SMSPlanId = "VIP", Cost = 0, Messages = 0 };

            db.SmsPlans.Add(spLow);
            db.SmsPlans.Add(spHigh);
            db.SmsPlans.Add(sp25SoloRec);
            db.SmsPlans.Add(spVIP);
            db.SaveChanges();

            spLow       = db.SmsPlans.FirstOrDefault(m => m.SMSPlanId == "Low");
            spHigh      = db.SmsPlans.FirstOrDefault(m => m.SMSPlanId == "High");
            sp25SoloRec = db.SmsPlans.FirstOrDefault(m => m.SMSPlanId == "25SoloRec");
            spVIP       = db.SmsPlans.FirstOrDefault(m => m.SMSPlanId == "VIP");

            // Data Plans
            DataPlan dpMMSStandardt = new DataPlan() { DataPlanId = "PAQUETE MMS STANDART", Cost = 0, Data = 0 };
            DataPlan dp600MB = new DataPlan() { DataPlanId = "PAQUETE DATOS 600MB 100K", Cost = 8, Data = 600 };
            DataPlan dp900MB = new DataPlan() { DataPlanId = "PAQUETE DATOS 900MB 100K", Cost = 12, Data = 900 };
            DataPlan dp1_5GB = new DataPlan() { DataPlanId = "PAQUETE DATOS 1.5GB 100K", Cost = 15, Data = 1536 };
            DataPlan dp3GB = new DataPlan() { DataPlanId = "PAQUETE DATOS 3GB 100K", Cost = 25, Data = 3072 };
            DataPlan dp4GB = new DataPlan() { DataPlanId = "PAQUETE DATOS 4GB 100K", Cost = 30, Data = 4096 };
            DataPlan dp7GB = new DataPlan() { DataPlanId = "PAQUETE DATOS 7GB 100K", Cost = 35, Data = 7168 };
            DataPlan dp10_5GB = new DataPlan() { DataPlanId = "PAQUETE DATOS 10.5GB 100K", Cost = 45, Data = 10752 };

            db.DataPlans.Add(dpMMSStandardt);
            db.DataPlans.Add(dp600MB);
            db.DataPlans.Add(dp900MB);
            db.DataPlans.Add(dp1_5GB);
            db.DataPlans.Add(dp3GB);
            db.DataPlans.Add(dp4GB);
            db.DataPlans.Add(dp7GB);
            db.DataPlans.Add(dp10_5GB);
            db.SaveChanges();

            dpMMSStandardt = db.DataPlans.FirstOrDefault(m => m.DataPlanId == "PAQUETE MMS STANDART");
            dp600MB  = db.DataPlans.FirstOrDefault(m => m.DataPlanId == "PAQUETE DATOS 600MB 100K");
            dp900MB  = db.DataPlans.FirstOrDefault(m => m.DataPlanId == "PAQUETE DATOS 900MB 100K");
            dp1_5GB  = db.DataPlans.FirstOrDefault(m => m.DataPlanId == "PAQUETE DATOS 1.5GB 100K");
            dp3GB    = db.DataPlans.FirstOrDefault(m => m.DataPlanId == "PAQUETE DATOS 3GB 100K");
            dp4GB    = db.DataPlans.FirstOrDefault(m => m.DataPlanId == "PAQUETE DATOS 4GB 100K");
            dp7GB    = db.DataPlans.FirstOrDefault(m => m.DataPlanId == "PAQUETE DATOS 7GB 100K");
            dp10_5GB = db.DataPlans.FirstOrDefault(m => m.DataPlanId == "PAQUETE DATOS 10.5GB 100K");
            // data plan assignment

            // phonelines

            // Employees sin mobil
            Employee en1 = new Employee() { Name = "Abel Fernandez Ortega", CostCenterCode = "29", Email = "abel.fernandez@mcvcomercial.cu" };
            Employee en2 = new Employee() { Name = "Adelaida Martínez Godoy", Extension = "701", PersonalCode = "310516", CostCenterCode = "04", Email = "recepcion.mcv@mcvcomercial.cu" };
            Employee en3 = new Employee() { Name = "Alberto de la Torre", Extension = "844", CostCenterCode = "20", Email = "almacen.pintura@mcvcomercial.cu" };
            Employee en4 = new Employee() { Name = "Alexander Guerra Hernandez", CostCenterCode = "31" };
            Employee en5 = new Employee() { Name = "Alexei Duque", CostCenterCode = "20", Email = "alexei.duque@mcvcomercial.cu" };
            Employee en6 = new Employee() { Name = "Ana Belen Perez", Extension = "749", PersonalCode = "051119", CostCenterCode = "12", Email = "ana.perez@mcvcomercial.cu" };
            Employee en7 = new Employee() { Name = "Angela Batista", Email = "angela.batista@mcvcomercial.cu", CostCenterCode = "20", Extension = "899", PersonalCode = "150119" };
            Employee en8 = new Employee() { Name = "Antonio Li (Torre Control Lisa)", Email = "antonio.li@mcvcomercial.cu", CostCenterCode = "29" };
            Employee en9 = new Employee() { Name = "Arianne Alonso Figueroa", Email = "arianne.alonso@mcvcomercial.cu", CostCenterCode = "12", Extension = "742", PersonalCode = "100316" };
            Employee en10 = new Employee() { Name = "Arsenio A Cedeño Traba", Email = "arsenio.cedeno@mcvcomercial.cu", CostCenterCode = "01", Extension = "751", PersonalCode = "190319" };
            Employee en11 = new Employee() { Name = "Ayman Makram Sadek (ext)", CostCenterCode = "01", Extension = "799" };
            Employee en12 = new Employee() { Name = "Carlos A Prieto", CostCenterCode = "32" };
            Employee en13 = new Employee() { Name = "Claudia Soler", Email = "claudia.soler@mcvcomercial.cu", CostCenterCode = "20", Extension = "886", PersonalCode = "010223" };
            Employee en14 = new Employee() { Name = "Damaris Ercia Rodriguez-Mena", Email = "damarys.ercia@mcvcomercial.cu", CostCenterCode = "19" };
            Employee en15 = new Employee() { Name = "Dany Rivera", CostCenterCode = "32" };
            Employee en16 = new Employee() { Name = "Darien Gonzalez Brito", CostCenterCode = "32" };
            Employee en17 = new Employee() { Name = "Dayron Perez", Email = "dayron.perez@mcvcomercial.cu", CostCenterCode = "12", Extension = "741", PersonalCode = "040712" };
            Employee en18 = new Employee() { Name = "Denisse Lopez", Email = "denisse.lopez@mcvcomercial.cu", CostCenterCode = "20" };
            Employee en19 = new Employee() { Name = "Diego (Comedor)", CostCenterCode = "04", Extension = "867", PersonalCode = "870398" };
            Employee en20 = new Employee() { Name = "Edris Guerra", CostCenterCode = "06" };
            Employee en21 = new Employee() { Name = "Enrique Llopiz", Email = "enrique.llopiz@mcvcomercial.cu", CostCenterCode = "01" };
            Employee en22 = new Employee() { Name = "Eva Barbara Parsons", Email = "eva.parsons@mcvcomercial.cu", CostCenterCode = "05", Extension = "718", PersonalCode = "570528" };
            Employee en23 = new Employee() { Name = "Gilberto Gomez", Email = "gilberto.gomez@mcvcomercial.cu", CostCenterCode = "29", Extension = "808" };
            Employee en24 = new Employee() { Name = "Heriberto Leal", CostCenterCode = "32" };
            Employee en25 = new Employee() { Name = "Ines Medina", CostCenterCode = "04", Extension = "852", PersonalCode = "947720" };
            Employee en26 = new Employee() { Name = "Jacinto Vazquez", Email = "jacinto.vazquez@mcvcomercial.cu", CostCenterCode = "20", Extension = "845", PersonalCode = "170518" };
            Employee en27 = new Employee() { Name = "Joaquin Medina", Email = "joaquin.medina@mcvcomercial.cu", CostCenterCode = "29", Extension = "807", PersonalCode = "177006" };
            Employee en28 = new Employee() { Name = "Johan Morgado Estela", Email = "johan.morgado@mcvcomercial.cu", CostCenterCode = "20", PersonalCode = "831005" };
            Employee en29 = new Employee() { Name = "Jorge Torres Rodriguez", CostCenterCode = "33" };
            Employee en30 = new Employee() { Name = "Jose Deus", Email = "jose.deus@mcvcomercial.cu", CostCenterCode = "12", Extension = "739", PersonalCode = "093303" };
            Employee en31 = new Employee() { Name = "Jose Enamorado Trujillo", CostCenterCode = "19" };
            Employee en32 = new Employee() { Name = "Juan Cruz", Email = "juan.cruz@mcvcomercial.cu", CostCenterCode = "29", Extension = "806", PersonalCode = "462091" };
            Employee en33 = new Employee() { Name = "Juan Rolando Ramírez Santos", Email = "rolando.ramirez@mcvcomercial.cu", CostCenterCode = "36", Extension = "890", PersonalCode = "208787" };
            Employee en34 = new Employee() { Name = "Julio Javier Avila Llorente", Email = "julio.avila@mcvcomercial.cu", CostCenterCode = "20", Extension = "846", PersonalCode = "250517" };
            Employee en35 = new Employee() { Name = "Julio Pedroso", Email = "julio.pedroso@mcvcomercial.cu", CostCenterCode = "12", Extension = "745", PersonalCode = "800899" };
            Employee en36 = new Employee() { Name = "Lazaro Reyes", Email = "lazaro.reyes@mcvcomercial.cu", CostCenterCode = "20", PersonalCode = "250118" };
            Employee en37 = new Employee() { Name = "Lazaro Hernandez", Email = "lazaro.hernandez@mcvcomercial.cu", CostCenterCode = "35" };
            Employee en38 = new Employee() { Name = "Leonardo Miyar", Email = "leonardo.miyar@mcvcomercial.cu", CostCenterCode = "12", PersonalCode = "679601" };
            Employee en39 = new Employee() { Name = "Leonardo de la Nuez", Email = "leonardo.delanuez@mcvcomercial.cu", CostCenterCode = "19" };
            Employee en40 = new Employee() { Name = "Liu Oramas", Email = "liu.oramas@mcvcomercial.cu", CostCenterCode = "05", Extension = "717", PersonalCode = "150916" };
            Employee en41 = new Employee() { Name = "Luis Alberto Gonzalez", Email = "luis.gonzalez@mcvcomercial.cu", CostCenterCode = "21", Extension = "626" };
            Employee en42 = new Employee() { Name = "Luis Montero", CostCenterCode = "19" };
            Employee en43 = new Employee() { Name = "Mario A Jaime J Brig Industries", Email = "taller.pesados@mcvcomercial.cu", CostCenterCode = "33", Extension = "836", PersonalCode = "750412" };
            Employee en44 = new Employee() { Name = "Mario Castro", Email = "mario.castro@mcvcomercial.cu", CostCenterCode = "20" };
            Employee en45 = new Employee() { Name = "Mauricio Santa Cruz", Email = "mauricio.stcruz@mcvcomercial.cu", CostCenterCode = "06" };
            Employee en46 = new Employee() { Name = "Mauro Gorgas", Email = "mauro.gorgas@mcvcomercial.cu", CostCenterCode = "36", Extension = "813", PersonalCode = "448776" };
            Employee en47 = new Employee() { Name = "Miguel A Gil Perez", Email = "miguel.gil@mcvcomercial.cu", CostCenterCode = "37" };
            Employee en48 = new Employee() { Name = "Lourdes Otero", Email = "lourdes.otero@mcvcomercial.cu", CostCenterCode = "20", Extension = "896", PersonalCode = "30516"/*revisar*/ };
            Employee en49 = new Employee() { Name = "Miguel Hernandez Leiro", Email = "panol.filial2@mcvcomercial.cu", CostCenterCode = "32" };
            Employee en50 = new Employee() { Name = "Miriam Isabel Coayo", Email = "miriam.coayo@mcvcomercial.cu", CostCenterCode = "12", Extension = "744", PersonalCode = "071016" };
            Employee en51 = new Employee() { Name = "Nicanor Santiesteban Olivero", Email = "nicanor.santiesteban@mcvcomercial.cu", CostCenterCode = "06" };
            Employee en52 = new Employee() { Name = "Nicolás Aguirre", Email = "nicolas.aguirre@mcvcomercial.cu", CostCenterCode = "21", Extension = "616" };
            Employee en53 = new Employee() { Name = "Niurka Fernandez Garcia", Email = "niurka.fernandez@mcvcomercial.cu", CostCenterCode = "12", Extension = "750", PersonalCode = "081119" };
            Employee en54 = new Employee() { Name = "Omar A Rodriguez", Email = "omar.rodriguez@mcvcomercial.cu", CostCenterCode = "20", Extension = "893", };
            Employee en55 = new Employee() { Name = "Omar Medina", Email = "omar.medina@mcvcomercial.cu", CostCenterCode = "19", Extension = "610" };
            Employee en56 = new Employee() { Name = "Oscar Alberto Medina", Email = "oscar.medina@mcvcomercial.cu", CostCenterCode = "35", Extension = "874" };
            Employee en57 = new Employee() { Name = "Pañol MTU", Email = "panol.mtu@mcvcomercial.cu", CostCenterCode = "19" };
            Employee en58 = new Employee() { Name = "Pedro Antonio Escalona", CostCenterCode = "19" };
            Employee en59 = new Employee() { Name = "Raidel Batista", CostCenterCode = "19" };
            Employee en60 = new Employee() { Name = "Ramon Fernandez", Email = "ramon.fernandez@mcvcomercial.cu", CostCenterCode = "11", PersonalCode = "830422" };
            Employee en61 = new Employee() { Name = "Ransel Iznaga Ponvert", Email = "ransel.iznaga@mcvcomercial.cu", CostCenterCode = "01" };
            Employee en62 = new Employee() { Name = "Raulicer Zamora", Email = "raulicer.zamora@mcvcomercial.cu", CostCenterCode = "29", Extension = "833" };
            Employee en63 = new Employee() { Name = "Reynaldo Guerrero Estrada", Email = "reynaldo.guerrero@mcvcomercial.cu", CostCenterCode = "21" };
            Employee en64 = new Employee() { Name = "Roberto Carlos Balbin", Email = "roberto.balbin@mcvcomercial.cu", CostCenterCode = "20", PersonalCode = "298991" };
            Employee en65 = new Employee() { Name = "Roberto Delgado", Email = "roberto.delgado@mcvcomercial.cu", CostCenterCode = "20", Extension = "883", PersonalCode = "640415" };
            Employee en66 = new Employee() { Name = "Rodney Miranda", Email = "rodney.miranda@mcvcomercial.cu", CostCenterCode = "19" };
            Employee en67 = new Employee() { Name = "Rodolfo Michel", CostCenterCode = "31" };
            Employee en68 = new Employee() { Name = "Rolando Camero (Torre Control Berroa)", Email = "torre.control@mcvcomercial.cu", CostCenterCode = "29", Extension = "839", PersonalCode = "260218" };
            Employee en69 = new Employee() { Name = "Rolando Dávila", Email = "rolando.davila@mcvcomercial.cu", CostCenterCode = "16" };
            Employee en70 = new Employee() { Name = "Silvio Dorta", Email = "silvio.dorta@mcvcomercial.cu", CostCenterCode = "19" };
            Employee en71 = new Employee() { Name = "Vladimir Robaina Reyes", Email = "vladimir.robaina@mcvcomercial.cu", CostCenterCode = "20", Extension = "891" };
            Employee en72 = new Employee() { Name = "Wilmer Gutierrez Duarte", CostCenterCode = "32" };
            Employee en73 = new Employee() { Name = "Yanet Peña", Email = "yanet.pena@mcvcomercial.cu", CostCenterCode = "12", Extension = "738", PersonalCode = "530391" };
            Employee en74 = new Employee() { Name = "Yonmara Casañas", Email = "yonmara.casanas@mcvcomercial.cu", CostCenterCode = "21", Extension = "625" };
            Employee en75 = new Employee() { Name = "Yoslaidy Pérez", Email = "yoslaidy.perez@mcvcomercial.cu", CostCenterCode = "11", };
            Employee en76 = new Employee() { Name = "Yusmil Benitez Reyes", Email = "yusmil.benitez@mcvcomercial.cu", CostCenterCode = "12", Extension = "740", PersonalCode = "090317" };
            Employee en77 = new Employee() { Name = "Carlos Chicha", Email = "carlos.chicha@mcvcomercial.cu", CostCenterCode = "20", Extension = "879" };
            //
            db.Employees.Add(en1);
            db.Employees.Add(en2);
            db.Employees.Add(en3);
            db.Employees.Add(en4);
            db.Employees.Add(en5);
            db.Employees.Add(en6);
            db.Employees.Add(en7);
            db.Employees.Add(en8);
            db.Employees.Add(en9);
            db.Employees.Add(en10);
            db.Employees.Add(en11);
            db.Employees.Add(en12);
            db.Employees.Add(en13);
            db.Employees.Add(en14);
            db.Employees.Add(en15);
            db.Employees.Add(en16);
            db.Employees.Add(en17);
            db.Employees.Add(en18);
            db.Employees.Add(en19);
            db.Employees.Add(en20);
            db.Employees.Add(en21);
            db.Employees.Add(en22);
            db.Employees.Add(en23);
            db.Employees.Add(en24);
            db.Employees.Add(en25);
            db.Employees.Add(en26);
            db.Employees.Add(en27);
            db.Employees.Add(en28);
            db.Employees.Add(en29);
            db.Employees.Add(en30);
            db.Employees.Add(en31);
            db.Employees.Add(en32);
            db.Employees.Add(en33);
            db.Employees.Add(en34);
            db.Employees.Add(en35);
            db.Employees.Add(en36);
            db.Employees.Add(en37);
            db.Employees.Add(en38);
            db.Employees.Add(en39);
            db.Employees.Add(en40);
            db.Employees.Add(en41);
            db.Employees.Add(en42);
            db.Employees.Add(en43);
            db.Employees.Add(en44);
            db.Employees.Add(en45);
            db.Employees.Add(en46);
            db.Employees.Add(en47);
            db.Employees.Add(en48);
            db.Employees.Add(en49);
            db.Employees.Add(en50);
            db.Employees.Add(en51);
            db.Employees.Add(en52);
            db.Employees.Add(en53);
            db.Employees.Add(en54);
            db.Employees.Add(en55);
            db.Employees.Add(en56);
            db.Employees.Add(en57);
            db.Employees.Add(en58);
            db.Employees.Add(en59);
            db.Employees.Add(en60);
            db.Employees.Add(en61);
            db.Employees.Add(en62);
            db.Employees.Add(en63);
            db.Employees.Add(en64);
            db.Employees.Add(en65);
            db.Employees.Add(en66);
            db.Employees.Add(en67);
            db.Employees.Add(en68);
            db.Employees.Add(en69);
            db.Employees.Add(en70);
            db.Employees.Add(en71);
            db.Employees.Add(en72);
            db.Employees.Add(en73);
            db.Employees.Add(en74);
            db.Employees.Add(en75);
            db.Employees.Add(en76);
            db.Employees.Add(en77);
            db.SaveChanges();
            //               

            //employees con movil
            Employee e1 = new Employee() { Name = "Ada Elena López", Extension = "737", PersonalCode = "805714", CostCenterCode = "12", Email = "ada.lopez@mcvcomercial.cu" }; //52851772
            PhoneLine pl1 = new PhoneLine() { PhoneNumber = "5352861772", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "1528665", PIN = 5635, PUK = 62851562 };
            db.Employees.Add(e1);
            db.PhoneLines.Add(pl1);
            db.SaveChanges();
            e1 = db.Employees.FirstOrDefault(m => m.Name == "Ada Elena López");
            pl1 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352861772");
            PhoneLineEmployee ple1 = new PhoneLineEmployee() { EmployeeId = e1.EmployeeId, PhoneNumber = pl1.PhoneNumber };
            SMSPlanAssignment spa1 = new SMSPlanAssignment() { PhoneNumber = pl1.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple1);
            db.SmsPlanAssignments.Add(spa1);
            db.SaveChanges();
            //
            Employee e2 = new Employee() { Name = "Adian Soto", CostCenterCode = "19" }; //52871933
            PhoneLine pl2 = new PhoneLine() { PhoneNumber = "5352871933", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "819031", PIN = 6379, PUK = 57159972 };
            db.Employees.Add(e2);
            db.PhoneLines.Add(pl2);
            db.SaveChanges();
            e2 = db.Employees.FirstOrDefault(m => m.Name == "Adian Soto");
            pl2 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352871933");
            PhoneLineEmployee ple2 = new PhoneLineEmployee() { EmployeeId = e2.EmployeeId, PhoneNumber = pl2.PhoneNumber };
            SMSPlanAssignment spa2 = new SMSPlanAssignment() { PhoneNumber = pl2.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = sp25SoloRec.SMSPlanId };
            db.PhoneLineEmployees.Add(ple2);
            db.SmsPlanAssignments.Add(spa2);
            db.SaveChanges();
            //
            Employee e3 = new Employee() { Name = "Adolfo Cepero", Extension = "731", PersonalCode = "746426", CostCenterCode = "12", Email = "adolfo.cepero@mcvcomercial.cu" }; //52809957
            PhoneLine pl3 = new PhoneLine() { PhoneNumber = "5352809957", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "925606", PIN = 3323, PUK = 53899564 };
            db.Employees.Add(e3);
            db.PhoneLines.Add(pl3);
            db.SaveChanges();
            e3 = db.Employees.FirstOrDefault(m => m.Name == "Adolfo Cepero");
            pl3 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352809957");
            PhoneLineEmployee ple3 = new PhoneLineEmployee() { EmployeeId = e3.EmployeeId, PhoneNumber = pl3.PhoneNumber };
            SMSPlanAssignment spa3 = new SMSPlanAssignment() { PhoneNumber = pl3.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple3);
            db.SmsPlanAssignments.Add(spa3);
            db.SaveChanges();
            //
            Employee e4 = new Employee() { Name = "Adrian Brizuela Gonzalez", Extension = "743", PersonalCode = "130216", CostCenterCode = "12", Email = "adrian.brizuela@mcvcomercial.cu" }; //52855829
            PhoneLine pl4 = new PhoneLine() { PhoneNumber = "5352855829", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "367117", PIN = 4469, PUK = 79438885 };
            db.Employees.Add(e4);
            db.PhoneLines.Add(pl4);
            db.SaveChanges();
            e4 = db.Employees.FirstOrDefault(m => m.Name == "Adrian Brizuela Gonzalez");
            pl4 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352855829");
            PhoneLineEmployee ple4 = new PhoneLineEmployee() { EmployeeId = e4.EmployeeId, PhoneNumber = pl4.PhoneNumber };
            SMSPlanAssignment spa4 = new SMSPlanAssignment() { PhoneNumber = pl4.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple4);
            db.SmsPlanAssignments.Add(spa4);
            db.SaveChanges();
            //
            Employee e5 = new Employee() { Name = "Agusnay Salvador", PersonalCode = "101116", CostCenterCode = "07" }; //52121433
            PhoneLine pl5 = new PhoneLine() { PhoneNumber = "5352121433", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "5399954", PIN = 2465, PUK = 33144342 };
            db.Employees.Add(e5);
            db.PhoneLines.Add(pl5);
            db.SaveChanges();
            e5 = db.Employees.FirstOrDefault(m => m.Name == "Agusnay Salvador");
            pl5 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352121433");
            PhoneLineEmployee ple5 = new PhoneLineEmployee() { EmployeeId = e5.EmployeeId, PhoneNumber = pl5.PhoneNumber };
            SMSPlanAssignment spa5 = new SMSPlanAssignment() { PhoneNumber = pl5.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple5);
            db.SmsPlanAssignments.Add(spa5);
            db.SaveChanges();
            //
            Employee e6 = new Employee() { Name = "Alain Gonzalez Rodriguez", Extension = "818", PersonalCode = "021216", CostCenterCode = "36", Email = "alain.gonzalez@mcvcomercial.cu" }; //52631709
            PhoneLine pl6 = new PhoneLine() { PhoneNumber = "5352631709", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "237741", PIN = 126, PUK = 91349717 };
            db.Employees.Add(e6);
            db.PhoneLines.Add(pl6);
            db.SaveChanges();
            e6 = db.Employees.FirstOrDefault(m => m.Name == "Alain Gonzalez Rodriguez");
            pl6 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352631709");
            PhoneLineEmployee ple6 = new PhoneLineEmployee() { EmployeeId = e6.EmployeeId, PhoneNumber = pl6.PhoneNumber };
            SMSPlanAssignment spa6 = new SMSPlanAssignment() { PhoneNumber = pl6.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple6);
            db.SmsPlanAssignments.Add(spa6);
            db.SaveChanges();
            //
            Employee e7 = new Employee() { Name = "Alain Rodriguez", CostCenterCode = "19", Email = "alain.rodriguez@mcvcomercial.cu" }; //52631213
            PhoneLine pl7 = new PhoneLine() { PhoneNumber = "5352631213", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "243279", PIN = 1155, PUK = 7134408 };
            db.Employees.Add(e7);
            db.PhoneLines.Add(pl7);
            db.SaveChanges();
            e7 = db.Employees.FirstOrDefault(m => m.Name == "Alain Rodriguez");
            pl7 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352631213");
            PhoneLineEmployee ple7 = new PhoneLineEmployee() { EmployeeId = e7.EmployeeId, PhoneNumber = pl7.PhoneNumber };
            SMSPlanAssignment spa7 = new SMSPlanAssignment() { PhoneNumber = pl7.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple7);
            db.SmsPlanAssignments.Add(spa7);
            db.SaveChanges();
            //
            Employee e8 = new Employee() { Name = "Alberto Fernandez", CostCenterCode = "19" }; //52878652
            PhoneLine pl8 = new PhoneLine() { PhoneNumber = "5352878652", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "670868", PIN = 2400, PUK = 64310669 };
            db.Employees.Add(e8);
            db.PhoneLines.Add(pl8);
            db.SaveChanges();
            e8 = db.Employees.FirstOrDefault(m => m.Name == "Alberto Fernandez");
            pl8 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352878652");
            PhoneLineEmployee ple8 = new PhoneLineEmployee() { EmployeeId = e8.EmployeeId, PhoneNumber = pl8.PhoneNumber };
            SMSPlanAssignment spa8 = new SMSPlanAssignment() { PhoneNumber = pl8.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = sp25SoloRec.SMSPlanId };
            db.PhoneLineEmployees.Add(ple8);
            db.SmsPlanAssignments.Add(spa8);
            db.SaveChanges();
            //
            Employee e9 = new Employee() { Name = "Alberto Ramos", CostCenterCode = "01", Email = "alberto.ramos@mcvcomercial.cu" }; //52151907
            PhoneLine pl9 = new PhoneLine() { PhoneNumber = "5352151907", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "6425317", PIN = 6870, PUK = 8041409 };
            db.Employees.Add(e9);
            db.PhoneLines.Add(pl9);
            db.SaveChanges();
            e9 = db.Employees.FirstOrDefault(m => m.Name == "Alberto Ramos");
            pl9 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352151907");
            PhoneLineEmployee ple9 = new PhoneLineEmployee() { EmployeeId = e9.EmployeeId, PhoneNumber = pl9.PhoneNumber };
            SMSPlanAssignment spa9 = new SMSPlanAssignment() { PhoneNumber = pl9.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple9);
            db.SmsPlanAssignments.Add(spa9);
            db.SaveChanges();
            //
            Employee e10 = new Employee() { Name = "Alejandro Martinez Bayar", CostCenterCode = "33" }; //52134600
            PhoneLine pl10 = new PhoneLine() { PhoneNumber = "5352134600", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "5480809", PIN = 4328, PUK = 86292502 };
            db.Employees.Add(e10);
            db.PhoneLines.Add(pl10);
            db.SaveChanges();
            e10 = db.Employees.FirstOrDefault(m => m.Name == "Alejandro Martinez Bayar");
            pl10 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352134600");
            PhoneLineEmployee ple10 = new PhoneLineEmployee() { EmployeeId = e10.EmployeeId, PhoneNumber = pl10.PhoneNumber };
            SMSPlanAssignment spa10 = new SMSPlanAssignment() { PhoneNumber = pl10.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple10);
            db.SmsPlanAssignments.Add(spa10);
            db.SaveChanges();
            //
            Employee e11 = new Employee() { Name = "Alejandro Roque", CostCenterCode = "20" }; //52879991
            PhoneLine pl11 = new PhoneLine() { PhoneNumber = "5352879991", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "670868", PIN = 4245, PUK = 72328875 };
            db.Employees.Add(e11);
            db.PhoneLines.Add(pl11);
            db.SaveChanges();
            e11 = db.Employees.FirstOrDefault(m => m.Name == "Alejandro Roque");
            pl11 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352879991");
            PhoneLineEmployee ple11 = new PhoneLineEmployee() { EmployeeId = e11.EmployeeId, PhoneNumber = pl11.PhoneNumber };
            SMSPlanAssignment spa11 = new SMSPlanAssignment() { PhoneNumber = pl11.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = sp25SoloRec.SMSPlanId };
            db.PhoneLineEmployees.Add(ple11);
            db.SmsPlanAssignments.Add(spa11);
            db.SaveChanges();
            //
            Employee e12 = new Employee() { Name = "Alejandro Triana Torroledo", Extension = "810", PersonalCode = "091215", CostCenterCode = "36", Email = "alain.gonzalez@mcvcomercial.cu" }; //52631724
            PhoneLine pl12 = new PhoneLine() { PhoneNumber = "5352631724", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "237745", PIN = 1211, PUK = 28563316 };
            db.Employees.Add(e12);
            db.PhoneLines.Add(pl12);
            db.SaveChanges();
            e12 = db.Employees.FirstOrDefault(m => m.Name == "Alejandro Triana Torroledo");
            pl12 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352631724");
            PhoneLineEmployee ple12 = new PhoneLineEmployee() { EmployeeId = e12.EmployeeId, PhoneNumber = pl12.PhoneNumber };
            SMSPlanAssignment spa12 = new SMSPlanAssignment() { PhoneNumber = pl12.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spVIP.SMSPlanId };
            db.PhoneLineEmployees.Add(ple12);
            db.SmsPlanAssignments.Add(spa12);
            db.SaveChanges();
            //
            Employee e13 = new Employee() { Name = "Alejandro Téllez", Extension = "714", PersonalCode = "146292", CostCenterCode = "05", Email = "alejandro.tellez@mcvcomercial.cu" }; //52806508
            PhoneLine pl13 = new PhoneLine() { PhoneNumber = "5352806508", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "738768", PIN = 6068, PUK = 810726 };
            db.Employees.Add(e13);
            db.PhoneLines.Add(pl13);
            db.SaveChanges();
            e13 = db.Employees.FirstOrDefault(m => m.Name == "Alejandro Téllez");
            pl13 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352806508");
            PhoneLineEmployee ple13 = new PhoneLineEmployee() { EmployeeId = e13.EmployeeId, PhoneNumber = pl13.PhoneNumber };
            SMSPlanAssignment spa13 = new SMSPlanAssignment() { PhoneNumber = pl13.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple13);
            db.SmsPlanAssignments.Add(spa13);
            db.SaveChanges();
            //
            Employee e14 = new Employee() { Name = "Alexander Fuentes Diaz (JBrig Chapa)", Extension = "838", PersonalCode = "080218", CostCenterCode = "05", Email = "taller.chapisteria@mcvcomercial.cu" }; //52121786
            PhoneLine pl14 = new PhoneLine() { PhoneNumber = "5352121786", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "5480809", PIN = 1013, PUK = 19341820 };
            db.Employees.Add(e14);
            db.PhoneLines.Add(pl14);
            db.SaveChanges();
            e14 = db.Employees.FirstOrDefault(m => m.Name == "Alexander Fuentes Diaz (JBrig Chapa)");
            pl14 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352121786");
            PhoneLineEmployee ple14 = new PhoneLineEmployee() { EmployeeId = e14.EmployeeId, PhoneNumber = pl14.PhoneNumber };
            SMSPlanAssignment spa14 = new SMSPlanAssignment() { PhoneNumber = pl14.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple14);
            db.SmsPlanAssignments.Add(spa14);
            db.SaveChanges();
            //
            Employee e15 = new Employee() { Name = "Alexander Guerra", CostCenterCode = "31" }; //52151922
            PhoneLine pl15 = new PhoneLine() { PhoneNumber = "5352151922", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "6425317", PIN = 2011, PUK = 14769854 };
            db.Employees.Add(e15);
            db.PhoneLines.Add(pl15);
            db.SaveChanges();
            e15 = db.Employees.FirstOrDefault(m => m.Name == "Alexander Guerra");
            pl15 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352151922");
            PhoneLineEmployee ple15 = new PhoneLineEmployee() { EmployeeId = e15.EmployeeId, PhoneNumber = pl15.PhoneNumber };
            SMSPlanAssignment spa15 = new SMSPlanAssignment() { PhoneNumber = pl15.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple15);
            db.SmsPlanAssignments.Add(spa15);
            db.SaveChanges();
            //
            Employee e16 = new Employee() { Name = "Alexeis Grey Suarez", CostCenterCode = "33", Email = "alexeis.grey@mcvcomercial.cu" }; //52151920
            PhoneLine pl16 = new PhoneLine() { PhoneNumber = "5352151920", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "6425317", PIN = 5670, PUK = 35622925 };
            db.Employees.Add(e16);
            db.PhoneLines.Add(pl16);
            db.SaveChanges();
            e16 = db.Employees.FirstOrDefault(m => m.Name == "Alexeis Grey Suarez");
            pl16 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352151920");
            PhoneLineEmployee ple16 = new PhoneLineEmployee() { EmployeeId = e16.EmployeeId, PhoneNumber = pl16.PhoneNumber };
            SMSPlanAssignment spa16 = new SMSPlanAssignment() { PhoneNumber = pl16.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple16);
            db.SmsPlanAssignments.Add(spa16);
            db.SaveChanges();
            //
            Employee e17 = new Employee() { Name = "Alfredo Netfali Garcia", CostCenterCode = "01", Email = "alfredo.garcia@mcvcomercial.cu" }; //52112627
            PhoneLine pl17 = new PhoneLine() { PhoneNumber = "5352112627", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "4926530", PIN = 1023, PUK = 17172224 };
            db.Employees.Add(e17);
            db.PhoneLines.Add(pl17);
            db.SaveChanges();
            e17 = db.Employees.FirstOrDefault(m => m.Name == "Alfredo Netfali Garcia");
            pl17 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352112627");
            PhoneLineEmployee ple17 = new PhoneLineEmployee() { EmployeeId = e17.EmployeeId, PhoneNumber = pl17.PhoneNumber };
            SMSPlanAssignment spa17 = new SMSPlanAssignment() { PhoneNumber = pl17.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple17);
            db.SmsPlanAssignments.Add(spa17);
            db.SaveChanges();
            //
            Employee e18 = new Employee() { Name = "Andres Fundora Losada", Extension = "777", CostCenterCode = "01", Email = "andres.fundora@mcvcomercial.cu" }; //52112888
            PhoneLine pl18 = new PhoneLine() { PhoneNumber = "5352112888", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "4245901", PIN = 1574, PUK = 89051424 };
            db.Employees.Add(e18);
            db.PhoneLines.Add(pl18);
            db.SaveChanges();
            e18 = db.Employees.FirstOrDefault(m => m.Name == "Andres Fundora Losada");
            pl18 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352112888");
            PhoneLineEmployee ple18 = new PhoneLineEmployee() { EmployeeId = e18.EmployeeId, PhoneNumber = pl18.PhoneNumber };
            SMSPlanAssignment spa18 = new SMSPlanAssignment() { PhoneNumber = pl18.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple18);
            db.SmsPlanAssignments.Add(spa18);
            db.SaveChanges();
            //
            Employee e19 = new Employee() { Name = "Andres Silva", CostCenterCode = "36" }; //52850462
            PhoneLine pl19 = new PhoneLine() { PhoneNumber = "5352850462", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "272150", PIN = 1775, PUK = 28922285 };
            db.Employees.Add(e19);
            db.PhoneLines.Add(pl19);
            db.SaveChanges();
            e19 = db.Employees.FirstOrDefault(m => m.Name == "Andres Silva");
            pl19 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352850462");
            PhoneLineEmployee ple19 = new PhoneLineEmployee() { EmployeeId = e19.EmployeeId, PhoneNumber = pl19.PhoneNumber };
            SMSPlanAssignment spa19 = new SMSPlanAssignment() { PhoneNumber = pl19.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple19);
            db.SmsPlanAssignments.Add(spa19);
            db.SaveChanges();
            //
            Employee e20 = new Employee() { Name = "Andrés Helnández", Extension = "628", CostCenterCode = "19", PersonalCode = "931305", Email = "andres.hernandez@mcvcomercial.cu" }; //52803366
            PhoneLine pl20 = new PhoneLine() { PhoneNumber = "5352803366", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "268447", PIN = 1403, PUK = 8505442 };
            db.Employees.Add(e20);
            db.PhoneLines.Add(pl20);
            db.SaveChanges();
            e20 = db.Employees.FirstOrDefault(m => m.Name == "Andrés Helnández");
            pl20 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352803366");
            PhoneLineEmployee ple20 = new PhoneLineEmployee() { EmployeeId = e20.EmployeeId, PhoneNumber = pl20.PhoneNumber };
            SMSPlanAssignment spa20 = new SMSPlanAssignment() { PhoneNumber = pl20.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple20);
            db.SmsPlanAssignments.Add(spa20);
            db.SaveChanges();
            //
            Employee e21 = new Employee() { Name = "Andy Gonzalez Rodríguez", CostCenterCode = "36", PersonalCode = "120216", Email = "andy.gonzalez@mcvcomercial.cu" }; //52112631
            PhoneLine pl21 = new PhoneLine() { PhoneNumber = "5352112631", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "4926540", PIN = 8634, PUK = 92452326 };
            db.Employees.Add(e21);
            db.PhoneLines.Add(pl21);
            db.SaveChanges();
            e21 = db.Employees.FirstOrDefault(m => m.Name == "Andy Gonzalez Rodríguez");
            pl21 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352112631");
            PhoneLineEmployee ple21 = new PhoneLineEmployee() { EmployeeId = e21.EmployeeId, PhoneNumber = pl21.PhoneNumber };
            SMSPlanAssignment spa21 = new SMSPlanAssignment() { PhoneNumber = pl21.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple21);
            db.SmsPlanAssignments.Add(spa21);
            db.SaveChanges();
            //
            Employee e22 = new Employee() { Name = "Angel Cabrera", CostCenterCode = "20" }; //52166174
            PhoneLine pl22 = new PhoneLine() { PhoneNumber = "5352166174", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "3385832", PIN = 4904, PUK = 0 };
            db.Employees.Add(e22);
            db.PhoneLines.Add(pl22);
            db.SaveChanges();
            e22 = db.Employees.FirstOrDefault(m => m.Name == "Angel Cabrera");
            pl22 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352166174");
            PhoneLineEmployee ple22 = new PhoneLineEmployee() { EmployeeId = e22.EmployeeId, PhoneNumber = pl22.PhoneNumber };
            SMSPlanAssignment spa22 = new SMSPlanAssignment() { PhoneNumber = pl22.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple22);
            db.SmsPlanAssignments.Add(spa22);
            db.SaveChanges();
            //
            Employee e23 = new Employee() { Name = "Angel Castro", CostCenterCode = "06", Email = "angel.castro@mcvcomercial.cu", Extension = "798", PersonalCode = "700816" }; //52121431
            PhoneLine pl23 = new PhoneLine() { PhoneNumber = "5352121431", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "5399954", PIN = 5786, PUK = 98428090 };
            db.Employees.Add(e23);
            db.PhoneLines.Add(pl23);
            db.SaveChanges();
            e23 = db.Employees.FirstOrDefault(m => m.Name == "Angel Castro");
            pl23 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352121431");
            PhoneLineEmployee ple23 = new PhoneLineEmployee() { EmployeeId = e23.EmployeeId, PhoneNumber = pl23.PhoneNumber };
            SMSPlanAssignment spa23 = new SMSPlanAssignment() { PhoneNumber = pl23.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple23);
            db.SmsPlanAssignments.Add(spa23);
            db.SaveChanges();
            //
            Employee e24 = new Employee() { Name = "Angel Hernández", CostCenterCode = "01", Email = "angel.hernandez@mcvcomercial.cu", Extension = "732", PersonalCode = "237145" }; //52151850
            PhoneLine pl24 = new PhoneLine() { PhoneNumber = "5352151850", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "6425317", PIN = 6668, PUK = 31008467 };
            db.Employees.Add(e24);
            db.PhoneLines.Add(pl24);
            db.SaveChanges();
            e24 = db.Employees.FirstOrDefault(m => m.Name == "Angel Hernández");
            pl24 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352151850");
            PhoneLineEmployee ple24 = new PhoneLineEmployee() { EmployeeId = e24.EmployeeId, PhoneNumber = pl24.PhoneNumber };
            SMSPlanAssignment spa24 = new SMSPlanAssignment() { PhoneNumber = pl24.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple24);
            db.SmsPlanAssignments.Add(spa24);
            db.SaveChanges();
            //
            Employee e25 = new Employee() { Name = "Antonio Cabrera García", Email = "antonio.cabrera@mcvcomercial.cu", CostCenterCode = "20", Extension = "898", PersonalCode = "201115" }; //59969797
            PhoneLine pl25 = new PhoneLine() { PhoneNumber = "5359969797", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "3385832", PIN = 3949, PUK = 9223942 };
            db.Employees.Add(e25);
            db.PhoneLines.Add(pl25);
            db.SaveChanges();
            e25 = db.Employees.FirstOrDefault(m => m.Name == "Antonio Cabrera García");
            pl25 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5359969797");
            PhoneLineEmployee ple25 = new PhoneLineEmployee() { EmployeeId = e25.EmployeeId, PhoneNumber = pl25.PhoneNumber };
            SMSPlanAssignment spa25 = new SMSPlanAssignment() { PhoneNumber = pl25.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple25);
            db.SmsPlanAssignments.Add(spa25);
            db.SaveChanges();
            //
            Employee e26 = new Employee() { Name = "Antonio Crespo", Email = "antonio.crepso@mcvcomercial.cu", CostCenterCode = "05", Extension = "713" }; //52803072
            PhoneLine pl26 = new PhoneLine() { PhoneNumber = "5352803072", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "237741", PIN = 5326, PUK = 61910222 };
            db.Employees.Add(e26);
            db.PhoneLines.Add(pl26);
            db.SaveChanges();
            e26 = db.Employees.FirstOrDefault(m => m.Name == "Antonio Crespo");
            pl26 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352803072");
            PhoneLineEmployee ple26 = new PhoneLineEmployee() { EmployeeId = e26.EmployeeId, PhoneNumber = pl26.PhoneNumber };
            SMSPlanAssignment spa26 = new SMSPlanAssignment() { PhoneNumber = pl26.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple26);
            db.SmsPlanAssignments.Add(spa26);
            db.SaveChanges();
            //
            Employee e27 = new Employee() { Name = "Ariel Garrido", Email = "ariel.garrido@mcvcomercial.cu", CostCenterCode = "35", Extension = "871", PersonalCode = "810331" }; //52093826
            PhoneLine pl27 = new PhoneLine() { PhoneNumber = "5352093826", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "4246001", PIN = 4623, PUK = 29054774 };
            db.Employees.Add(e27);
            db.PhoneLines.Add(pl27);
            db.SaveChanges();
            e27 = db.Employees.FirstOrDefault(m => m.Name == "Ariel Garrido");
            pl27 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352093826");
            PhoneLineEmployee ple27 = new PhoneLineEmployee() { EmployeeId = e27.EmployeeId, PhoneNumber = pl27.PhoneNumber };
            SMSPlanAssignment spa27 = new SMSPlanAssignment() { PhoneNumber = pl27.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple27);
            db.SmsPlanAssignments.Add(spa27);
            db.SaveChanges();
            //
            Employee e28 = new Employee() { Name = "Ariel Martinez", Email = "ariel.martinez@mcvcomercial.cu", CostCenterCode = "19" }; //59969800
            PhoneLine pl28 = new PhoneLine() { PhoneNumber = "5359969800", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "3385832", PIN = 1071, PUK = 98418555 };
            db.Employees.Add(e28);
            db.PhoneLines.Add(pl28);
            db.SaveChanges();
            e28 = db.Employees.FirstOrDefault(m => m.Name == "Ariel Martinez");
            pl28 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5359969800");
            PhoneLineEmployee ple28 = new PhoneLineEmployee() { EmployeeId = e28.EmployeeId, PhoneNumber = pl28.PhoneNumber };
            SMSPlanAssignment spa28 = new SMSPlanAssignment() { PhoneNumber = pl28.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple28);
            db.SmsPlanAssignments.Add(spa28);
            db.SaveChanges();
            //
            Employee e29 = new Employee() { Name = "Arnaldo Hernández", Email = "arnaldo.hernandez@mcvcomercial.cu", CostCenterCode = "19", Extension = "601" }; //52867980
            PhoneLine pl29 = new PhoneLine() { PhoneNumber = "5352867980", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "289782", PIN = 6025, PUK = 54411130 };
            db.Employees.Add(e29);
            db.PhoneLines.Add(pl29);
            db.SaveChanges();
            e29 = db.Employees.FirstOrDefault(m => m.Name == "Arnaldo Hernández");
            pl29 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352867980");
            PhoneLineEmployee ple29 = new PhoneLineEmployee() { EmployeeId = e29.EmployeeId, PhoneNumber = pl29.PhoneNumber };
            SMSPlanAssignment spa29 = new SMSPlanAssignment() { PhoneNumber = pl29.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple29);
            db.SmsPlanAssignments.Add(spa29);
            db.SaveChanges();
            //
            Employee e30 = new Employee() { Name = "Ayman Makram Sadek", Email = "ayman.makram@mcvcomercial.cu", CostCenterCode = "01", Extension = "711", PersonalCode = "111622" }; // 52630167 
            PhoneLine pl30 = new PhoneLine() { PhoneNumber = "5352630167", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "223096", PIN = 983, PUK = 3414089 };
            db.Employees.Add(e30);
            db.PhoneLines.Add(pl30);
            db.SaveChanges();
            e30 = db.Employees.FirstOrDefault(m => m.Name == "Ayman Makram Sadek");
            pl30 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352630167");
            PhoneLineEmployee ple30 = new PhoneLineEmployee() { EmployeeId = e30.EmployeeId, PhoneNumber = pl30.PhoneNumber };
            SMSPlanAssignment spa30 = new SMSPlanAssignment() { PhoneNumber = pl30.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spVIP.SMSPlanId };
            db.PhoneLineEmployees.Add(ple30);
            db.SmsPlanAssignments.Add(spa30);
            db.SaveChanges();
            //
            Employee e31 = new Employee() { Name = "Baden Bueno Lopez", Email = "baden.bueno@mcvcomercial.cu", CostCenterCode = "20", Extension = "880", PersonalCode = "241115" }; // 52163347
            PhoneLine pl31 = new PhoneLine() { PhoneNumber = "5352163347", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "3385832", PIN = 3497, PUK = 10613457 };
            db.Employees.Add(e31);
            db.PhoneLines.Add(pl31);
            db.SaveChanges();
            e31 = db.Employees.FirstOrDefault(m => m.Name == "Baden Bueno Lopez");
            pl31 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352163347");
            PhoneLineEmployee ple31 = new PhoneLineEmployee() { EmployeeId = e31.EmployeeId, PhoneNumber = pl31.PhoneNumber };
            SMSPlanAssignment spa31 = new SMSPlanAssignment() { PhoneNumber = pl31.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple31);
            db.SmsPlanAssignments.Add(spa31);
            db.SaveChanges();
            //
            Employee e32 = new Employee() { Name = "Barbara Belkis Hernandez", Email = "barbara.hernandez@mcvcomercial.cu", CostCenterCode = "36" }; // 52093825
            PhoneLine pl32 = new PhoneLine() { PhoneNumber = "5352093825", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "4245994", PIN = 5359, PUK = 17396657 };
            db.Employees.Add(e32);
            db.PhoneLines.Add(pl32);
            db.SaveChanges();
            e32 = db.Employees.FirstOrDefault(m => m.Name == "Barbara Belkis Hernandez");
            pl32 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352093825");
            PhoneLineEmployee ple32 = new PhoneLineEmployee() { EmployeeId = e32.EmployeeId, PhoneNumber = pl32.PhoneNumber };
            SMSPlanAssignment spa32 = new SMSPlanAssignment() { PhoneNumber = pl32.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple32);
            db.SmsPlanAssignments.Add(spa32);
            db.SaveChanges();
            //
            Employee e33 = new Employee() { Name = "Barbara Yanara Paz", Email = "barbara.paz@mcvcomercial.cu", CostCenterCode = "19", Extension = "621" }; // 5212628
            PhoneLine pl33 = new PhoneLine() { PhoneNumber = "5352112628", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "4926540", PIN = 2213, PUK = 7990447 };
            db.Employees.Add(e33);
            db.PhoneLines.Add(pl33);
            db.SaveChanges();
            e33 = db.Employees.FirstOrDefault(m => m.Name == "Barbara Yanara Paz");
            pl33 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352112628");
            PhoneLineEmployee ple33 = new PhoneLineEmployee() { EmployeeId = e33.EmployeeId, PhoneNumber = pl33.PhoneNumber };
            SMSPlanAssignment spa33 = new SMSPlanAssignment() { PhoneNumber = pl33.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple33);
            db.SmsPlanAssignments.Add(spa33);
            db.SaveChanges();
            //
            Employee e34 = new Employee() { Name = "Carlos Edier Tizon Garces", CostCenterCode = "06", PersonalCode = "110038" }; // 52121435
            PhoneLine pl34 = new PhoneLine() { PhoneNumber = "5352121435", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "5399954", PIN = 1110, PUK = 14172298 };
            db.Employees.Add(e34);
            db.PhoneLines.Add(pl34);
            db.SaveChanges();
            e34 = db.Employees.FirstOrDefault(m => m.Name == "Carlos Edier Tizon Garces");
            pl34 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352121435");
            PhoneLineEmployee ple34 = new PhoneLineEmployee() { EmployeeId = e34.EmployeeId, PhoneNumber = pl34.PhoneNumber };
            SMSPlanAssignment spa34 = new SMSPlanAssignment() { PhoneNumber = pl34.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple34);
            db.SmsPlanAssignments.Add(spa34);
            db.SaveChanges();
            //
            Employee e35 = new Employee() { Name = "Carlos Hernández", Email = "carlos.hernandez@mcvcomercial.cu", CostCenterCode = "19", Extension = "608" }; // 52852684
            PhoneLine pl35 = new PhoneLine() { PhoneNumber = "5352852684", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "254555", PIN = 3536, PUK = 66834133 };
            db.Employees.Add(e35);
            db.PhoneLines.Add(pl35);
            db.SaveChanges();
            e35 = db.Employees.FirstOrDefault(m => m.Name == "Carlos Hernández");
            pl35 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352852684");
            PhoneLineEmployee ple35 = new PhoneLineEmployee() { EmployeeId = e35.EmployeeId, PhoneNumber = pl35.PhoneNumber };
            SMSPlanAssignment spa35 = new SMSPlanAssignment() { PhoneNumber = pl35.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple35);
            db.SmsPlanAssignments.Add(spa35);
            db.SaveChanges();
            //
            Employee e36 = new Employee() { Name = "Carlos Martinez", Email = "carlos.martinez@mcvcomercial.cu", CostCenterCode = "29" }; // 52154195
            PhoneLine pl36 = new PhoneLine() { PhoneNumber = "5352154195", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "3385832", PIN = 1393, PUK = 70264347 };
            db.Employees.Add(e36);
            db.PhoneLines.Add(pl36);
            db.SaveChanges();
            e36 = db.Employees.FirstOrDefault(m => m.Name == "Carlos Martinez");
            pl36 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352154195");
            PhoneLineEmployee ple36 = new PhoneLineEmployee() { EmployeeId = e36.EmployeeId, PhoneNumber = pl36.PhoneNumber };
            SMSPlanAssignment spa36 = new SMSPlanAssignment() { PhoneNumber = pl36.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple36);
            db.SmsPlanAssignments.Add(spa36);
            db.SaveChanges();
            //
            Employee e37 = new Employee() { Name = "Carlos Viera", Email = "carlos.viera@mcvcomercial.cu", CostCenterCode = "20", Extension = "888", PersonalCode = "016193" }; // 52151912
            PhoneLine pl37 = new PhoneLine() { PhoneNumber = "5352151912", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "6425317", PIN = 4639, PUK = 26705522 };
            db.Employees.Add(e37);
            db.PhoneLines.Add(pl37);
            db.SaveChanges();
            e37 = db.Employees.FirstOrDefault(m => m.Name == "Carlos Viera");
            pl37 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352151912");
            PhoneLineEmployee ple37 = new PhoneLineEmployee() { EmployeeId = e37.EmployeeId, PhoneNumber = pl37.PhoneNumber };
            SMSPlanAssignment spa37 = new SMSPlanAssignment() { PhoneNumber = pl37.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple37);
            db.SmsPlanAssignments.Add(spa37);
            db.SaveChanges();
            //
            Employee e38 = new Employee() { Name = "Carlos de la Hoz Ortiz", CostCenterCode = "19" }; // 52871627
            PhoneLine pl38 = new PhoneLine() { PhoneNumber = "5352871627", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "772384", PIN = 3414, PUK = 47159566 };
            db.Employees.Add(e38);
            db.PhoneLines.Add(pl38);
            db.SaveChanges();
            e38 = db.Employees.FirstOrDefault(m => m.Name == "Carlos de la Hoz Ortiz");
            pl38 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352871627");
            PhoneLineEmployee ple38 = new PhoneLineEmployee() { EmployeeId = e38.EmployeeId, PhoneNumber = pl38.PhoneNumber };
            SMSPlanAssignment spa38 = new SMSPlanAssignment() { PhoneNumber = pl38.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = sp25SoloRec.SMSPlanId };
            db.PhoneLineEmployees.Add(ple38);
            db.SmsPlanAssignments.Add(spa38);
            db.SaveChanges();
            //
            Employee e39 = new Employee() { Name = "Claudio Fabian Brusa", Email = "claudio.brusa@mcvcomercial.cu", CostCenterCode = "20", Extension = "801" }; // 52804343
            PhoneLine pl39 = new PhoneLine() { PhoneNumber = "5352804343", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "26842", PIN = 354, PUK = 29173126 };
            db.Employees.Add(e39);
            db.PhoneLines.Add(pl39);
            db.SaveChanges();
            e39 = db.Employees.FirstOrDefault(m => m.Name == "Claudio Fabian Brusa");
            pl39 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352804343");
            PhoneLineEmployee ple39 = new PhoneLineEmployee() { EmployeeId = e39.EmployeeId, PhoneNumber = pl39.PhoneNumber };
            SMSPlanAssignment spa39 = new SMSPlanAssignment() { PhoneNumber = pl39.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple39);
            db.SmsPlanAssignments.Add(spa39);
            db.SaveChanges();
            //
            Employee e40 = new Employee() { Name = "Danger Perez", Email = "danger.perez@mcvcomercial.cu", CostCenterCode = "36", PersonalCode = "321420" }; // 52807904
            PhoneLine pl40 = new PhoneLine() { PhoneNumber = "5352807904", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "367117", PIN = 0, PUK = 75118012 };
            db.Employees.Add(e40);
            db.PhoneLines.Add(pl40);
            db.SaveChanges();
            e40 = db.Employees.FirstOrDefault(m => m.Name == "Danger Perez");
            pl40 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352807904");
            PhoneLineEmployee ple40 = new PhoneLineEmployee() { EmployeeId = e40.EmployeeId, PhoneNumber = pl40.PhoneNumber };
            SMSPlanAssignment spa40 = new SMSPlanAssignment() { PhoneNumber = pl40.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple40);
            db.SmsPlanAssignments.Add(spa40);
            db.SaveChanges();
            //
            Employee e41 = new Employee() { Name = "Daniel Pardo", Email = "daniel.pardo@mcvcomercial.cu", CostCenterCode = "19" }; // 52768888
            PhoneLine pl41 = new PhoneLine() { PhoneNumber = "5352768888", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "969560", PIN = 2593, PUK = 81503773 };
            db.Employees.Add(e41);
            db.PhoneLines.Add(pl41);
            db.SaveChanges();
            e41 = db.Employees.FirstOrDefault(m => m.Name == "Daniel Pardo");
            pl41 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352768888");
            PhoneLineEmployee ple41 = new PhoneLineEmployee() { EmployeeId = e41.EmployeeId, PhoneNumber = pl41.PhoneNumber };
            SMSPlanAssignment spa41 = new SMSPlanAssignment() { PhoneNumber = pl41.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = sp25SoloRec.SMSPlanId };
            db.PhoneLineEmployees.Add(ple41);
            db.SmsPlanAssignments.Add(spa41);
            db.SaveChanges();
            //
            Employee e42 = new Employee() { Name = "Digna Rosa Gamboa", Email = "digna.gamboa@mcvcomercial.cu", CostCenterCode = "01", Extension = "631", PersonalCode = "660384" }; // 52808020
            PhoneLine pl42 = new PhoneLine() { PhoneNumber = "5352808020", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "3385817", PIN = 6618, PUK = 42957093 };
            db.Employees.Add(e42);
            db.PhoneLines.Add(pl42);
            db.SaveChanges();
            e42 = db.Employees.FirstOrDefault(m => m.Name == "Digna Rosa Gamboa");
            pl42 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352808020");
            PhoneLineEmployee ple42 = new PhoneLineEmployee() { EmployeeId = e42.EmployeeId, PhoneNumber = pl42.PhoneNumber };
            SMSPlanAssignment spa42 = new SMSPlanAssignment() { PhoneNumber = pl42.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple42);
            db.SmsPlanAssignments.Add(spa42);
            db.SaveChanges();
            //
            Employee e43 = new Employee() { Name = "Dinorah Palay Zamora", Email = "dinorah.palay@mcvcomercial.cu", CostCenterCode = "12", Extension = "748", PersonalCode = "435560" }; // 52145160
            PhoneLine pl43 = new PhoneLine() { PhoneNumber = "5352145160", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "232082", PIN = 3202, PUK = 74133248 };
            db.Employees.Add(e43);
            db.PhoneLines.Add(pl43);
            db.SaveChanges();
            e43 = db.Employees.FirstOrDefault(m => m.Name == "Dinorah Palay Zamora");
            pl43 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352145160");
            PhoneLineEmployee ple43 = new PhoneLineEmployee() { EmployeeId = e43.EmployeeId, PhoneNumber = pl43.PhoneNumber };
            SMSPlanAssignment spa43 = new SMSPlanAssignment() { PhoneNumber = pl43.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple43);
            db.SmsPlanAssignments.Add(spa43);
            db.SaveChanges();
            //
            Employee e44 = new Employee() { Name = "Dionny Williams Fdez", Email = "dionny.williams@mcvcomercial.cu", CostCenterCode = "19" }; // 52127026
            PhoneLine pl44 = new PhoneLine() { PhoneNumber = "5352127026", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "278944", PIN = 1486, PUK = 24241118 };
            db.Employees.Add(e44);
            db.PhoneLines.Add(pl44);
            db.SaveChanges();
            e44 = db.Employees.FirstOrDefault(m => m.Name == "Dionny Williams Fdez");
            pl44 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352127026");
            PhoneLineEmployee ple44 = new PhoneLineEmployee() { EmployeeId = e44.EmployeeId, PhoneNumber = pl44.PhoneNumber };
            SMSPlanAssignment spa44 = new SMSPlanAssignment() { PhoneNumber = pl44.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple44);
            db.SmsPlanAssignments.Add(spa44);
            db.SaveChanges();
            //
            Employee e45 = new Employee() { Name = "Eduardo Acevedo", Email = "eduardo.acevedo@mcvcomercial.cu", CostCenterCode = "13", Extension = "762", PersonalCode = "327687" }; // 52855844
            PhoneLine pl45 = new PhoneLine() { PhoneNumber = "5352855844", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "766734", PIN = 9947, PUK = 49327840 };
            db.Employees.Add(e45);
            db.PhoneLines.Add(pl45);
            db.SaveChanges();
            e45 = db.Employees.FirstOrDefault(m => m.Name == "Eduardo Acevedo");
            pl45 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352855844");
            PhoneLineEmployee ple45 = new PhoneLineEmployee() { EmployeeId = e45.EmployeeId, PhoneNumber = pl45.PhoneNumber };
            SMSPlanAssignment spa45 = new SMSPlanAssignment() { PhoneNumber = pl45.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple45);
            db.SmsPlanAssignments.Add(spa45);
            db.SaveChanges();
            //
            Employee e46 = new Employee() { Name = "Eduardo Santos", Email = "eduardo.santos@mcvcomercial.cu", CostCenterCode = "19" }; // 52142181
            PhoneLine pl46 = new PhoneLine() { PhoneNumber = "5352142181", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "6240174", PIN = 8648, PUK = 95280836 };
            db.Employees.Add(e46);
            db.PhoneLines.Add(pl46);
            db.SaveChanges();
            e46 = db.Employees.FirstOrDefault(m => m.Name == "Eduardo Santos");
            pl46 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352142181");
            PhoneLineEmployee ple46 = new PhoneLineEmployee() { EmployeeId = e46.EmployeeId, PhoneNumber = pl46.PhoneNumber };
            SMSPlanAssignment spa46 = new SMSPlanAssignment() { PhoneNumber = pl46.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple46);
            db.SmsPlanAssignments.Add(spa46);
            db.SaveChanges();
            //
            Employee e47 = new Employee() { Name = "Emilio Gomez Gonzalez", CostCenterCode = "19" }; // 52142181
            PhoneLine pl47 = new PhoneLine() { PhoneNumber = "5352800643", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "254071", PIN = 5238, PUK = 58715346 };
            db.Employees.Add(e47);
            db.PhoneLines.Add(pl47);
            db.SaveChanges();
            e47 = db.Employees.FirstOrDefault(m => m.Name == "Emilio Gomez Gonzalez");
            pl47 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352800643");
            PhoneLineEmployee ple47 = new PhoneLineEmployee() { EmployeeId = e47.EmployeeId, PhoneNumber = pl47.PhoneNumber };
            SMSPlanAssignment spa47 = new SMSPlanAssignment() { PhoneNumber = pl47.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple47);
            db.SmsPlanAssignments.Add(spa47);
            db.SaveChanges();
            //
            Employee e48 = new Employee() { Name = "Emilio Roman", Email = "emilio.roman@mcvcomercial.cu", CostCenterCode = "07", PersonalCode = "626248" }; // 52792676
            PhoneLine pl48 = new PhoneLine() { PhoneNumber = "5352792676", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "814173", PIN = 6562, PUK = 15867910 };
            db.Employees.Add(e48);
            db.PhoneLines.Add(pl48);
            db.SaveChanges();
            e48 = db.Employees.FirstOrDefault(m => m.Name == "Emilio Roman");
            pl48 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352792676");
            PhoneLineEmployee ple48 = new PhoneLineEmployee() { EmployeeId = e48.EmployeeId, PhoneNumber = pl48.PhoneNumber };
            SMSPlanAssignment spa48 = new SMSPlanAssignment() { PhoneNumber = pl48.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple48);
            db.SmsPlanAssignments.Add(spa48);
            db.SaveChanges();
            //
            Employee e49 = new Employee() { Name = "Enmanuel de Jesus Hdez", CostCenterCode = "32" }; // 52151914
            PhoneLine pl49 = new PhoneLine() { PhoneNumber = "5352151914", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "6425317", PIN = 2960, PUK = 76756708 };
            db.Employees.Add(e49);
            db.PhoneLines.Add(pl49);
            db.SaveChanges();
            e49 = db.Employees.FirstOrDefault(m => m.Name == "Enmanuel De Jesus Hdez");
            pl49 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352151914");
            PhoneLineEmployee ple49 = new PhoneLineEmployee() { EmployeeId = e49.EmployeeId, PhoneNumber = pl49.PhoneNumber };
            SMSPlanAssignment spa49 = new SMSPlanAssignment() { PhoneNumber = pl49.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple49);
            db.SmsPlanAssignments.Add(spa49);
            db.SaveChanges();
            //
            Employee e50 = new Employee() { Name = "Eric Hernandez Perez", Email = "eric.hernandez@mcvcomercial.cu", CostCenterCode = "11", Extension = "758" }; // 52154206
            PhoneLine pl50 = new PhoneLine() { PhoneNumber = "5352154206", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "3385832", PIN = 8178, PUK = 22582759 };
            db.Employees.Add(e50);
            db.PhoneLines.Add(pl50);
            db.SaveChanges();
            e50 = db.Employees.FirstOrDefault(m => m.Name == "Eric Hernandez Perez");
            pl50 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352154206");
            PhoneLineEmployee ple50 = new PhoneLineEmployee() { EmployeeId = e50.EmployeeId, PhoneNumber = pl50.PhoneNumber };
            SMSPlanAssignment spa50 = new SMSPlanAssignment() { PhoneNumber = pl50.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple50);
            db.SmsPlanAssignments.Add(spa50);
            db.SaveChanges();
            //
            Employee e51 = new Employee() { Name = "Ernesto Hechavarria", Email = "ernesto.hechavarria@mcvcomercial.cu", CostCenterCode = "21", Extension = "615" }; // 52128807
            PhoneLine pl51 = new PhoneLine() { PhoneNumber = "5352128807", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "5671095", PIN = 3213, PUK = 45840585 };
            db.Employees.Add(e51);
            db.PhoneLines.Add(pl51);
            db.SaveChanges();
            e51 = db.Employees.FirstOrDefault(m => m.Name == "Ernesto Hechavarria");
            pl51 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352128807");
            PhoneLineEmployee ple51 = new PhoneLineEmployee() { EmployeeId = e51.EmployeeId, PhoneNumber = pl51.PhoneNumber };
            SMSPlanAssignment spa51 = new SMSPlanAssignment() { PhoneNumber = pl51.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple51);
            db.SmsPlanAssignments.Add(spa51);
            db.SaveChanges();
            //
            Employee e52 = new Employee() { Name = "Ernesto Diaz", Email = "ernesto.diaz@mcvcomercial.cu", CostCenterCode = "07", Extension = "775", PersonalCode = "251016" }; // 52802673
            PhoneLine pl52 = new PhoneLine() { PhoneNumber = "5352802673", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "998098", PIN = 5602, PUK = 53551796 };
            db.Employees.Add(e52);
            db.PhoneLines.Add(pl52);
            db.SaveChanges();
            e52 = db.Employees.FirstOrDefault(m => m.Name == "Ernesto Diaz");
            pl52 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352802673");
            PhoneLineEmployee ple52 = new PhoneLineEmployee() { EmployeeId = e52.EmployeeId, PhoneNumber = pl52.PhoneNumber };
            SMSPlanAssignment spa52 = new SMSPlanAssignment() { PhoneNumber = pl52.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple52);
            db.SmsPlanAssignments.Add(spa52);
            db.SaveChanges();
            //
            Employee e53 = new Employee() { Name = "Ernesto Posada Escoto", Email = "ernesto.escoto@mcvcomercial.cu", CostCenterCode = "13", Extension = "734", PersonalCode = "630326" }; // 52649029
            PhoneLine pl53 = new PhoneLine() { PhoneNumber = "5352649029", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "243279", PIN = 3924, PUK = 53984905 };
            db.Employees.Add(e53);
            db.PhoneLines.Add(pl53);
            db.SaveChanges();
            e53 = db.Employees.FirstOrDefault(m => m.Name == "Ernesto Posada Escoto");
            pl53 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352649029");
            PhoneLineEmployee ple53 = new PhoneLineEmployee() { EmployeeId = e53.EmployeeId, PhoneNumber = pl53.PhoneNumber };
            SMSPlanAssignment spa53 = new SMSPlanAssignment() { PhoneNumber = pl53.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple53);
            db.SmsPlanAssignments.Add(spa53);
            db.SaveChanges();
            //
            Employee e54 = new Employee() { Name = "Ernesto Posada Guardia", Email = "ernesto.posada@mcvcomercial.cu", CostCenterCode = "18", Extension = "756", PersonalCode = "108601" }; // 52869635
            PhoneLine pl54 = new PhoneLine() { PhoneNumber = "5352869635", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "258797", PIN = 4246, PUK = 25516411 };
            db.Employees.Add(e54);
            db.PhoneLines.Add(pl54);
            db.SaveChanges();
            e54 = db.Employees.FirstOrDefault(m => m.Name == "Ernesto Posada Guardia");
            pl54 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352869635");
            PhoneLineEmployee ple54 = new PhoneLineEmployee() { EmployeeId = e54.EmployeeId, PhoneNumber = pl54.PhoneNumber };
            SMSPlanAssignment spa54 = new SMSPlanAssignment() { PhoneNumber = pl54.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple54);
            db.SmsPlanAssignments.Add(spa54);
            db.SaveChanges();
            //
            Employee e55 = new Employee() { Name = "Eusebio Martínez", Email = "eusebio.martinez@mcvcomercial.cu", CostCenterCode = "19", Extension = "602" }; // 52630516
            PhoneLine pl55 = new PhoneLine() { PhoneNumber = "5352630516", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "243279", PIN = 8438, PUK = 14111514 };
            db.Employees.Add(e55);
            db.PhoneLines.Add(pl55);
            db.SaveChanges();
            e55 = db.Employees.FirstOrDefault(m => m.Name == "Eusebio Martínez");
            pl55 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352630516");
            PhoneLineEmployee ple55 = new PhoneLineEmployee() { EmployeeId = e55.EmployeeId, PhoneNumber = pl55.PhoneNumber };
            SMSPlanAssignment spa55 = new SMSPlanAssignment() { PhoneNumber = pl55.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple55);
            db.SmsPlanAssignments.Add(spa55);
            db.SaveChanges();
            //
            Employee e56 = new Employee() { Name = "Felix Mariano", Email = "felix.mariano@mcvcomercial.cu", CostCenterCode = "19" }; // 52166178
            PhoneLine pl56 = new PhoneLine() { PhoneNumber = "5352166178", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "3385832", PIN = 6815, PUK = 70477063 };
            db.Employees.Add(e56);
            db.PhoneLines.Add(pl56);
            db.SaveChanges();
            e56 = db.Employees.FirstOrDefault(m => m.Name == "Felix Mariano");
            pl56 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352166178");
            PhoneLineEmployee ple56 = new PhoneLineEmployee() { EmployeeId = e56.EmployeeId, PhoneNumber = pl56.PhoneNumber };
            SMSPlanAssignment spa56 = new SMSPlanAssignment() { PhoneNumber = pl56.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple56);
            db.SmsPlanAssignments.Add(spa56);
            db.SaveChanges();
            //
            Employee e57 = new Employee() { Name = "Fidel Rodriguez", Email = "mcv@enet.cu", CostCenterCode = "20" }; // 52856933
            PhoneLine pl57 = new PhoneLine() { PhoneNumber = "5352856933", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "260194", PIN = 3624, PUK = 83731567 };
            db.Employees.Add(e57);
            db.PhoneLines.Add(pl57);
            db.SaveChanges();
            e57 = db.Employees.FirstOrDefault(m => m.Name == "Fidel Rodriguez");
            pl57 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352856933");
            PhoneLineEmployee ple57 = new PhoneLineEmployee() { EmployeeId = e57.EmployeeId, PhoneNumber = pl57.PhoneNumber };
            SMSPlanAssignment spa57 = new SMSPlanAssignment() { PhoneNumber = pl57.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple57);
            db.SmsPlanAssignments.Add(spa57);
            db.SaveChanges();
            //
            Employee e58 = new Employee() { Name = "Francisco de León", Email = "francisco.deleon@mcvcomercial.cu", CostCenterCode = "19" }; // 52858342
            PhoneLine pl58 = new PhoneLine() { PhoneNumber = "5352858342", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "260189", PIN = 1786, PUK = 65768920 };
            db.Employees.Add(e58);
            db.PhoneLines.Add(pl58);
            db.SaveChanges();
            e58 = db.Employees.FirstOrDefault(m => m.Name == "Francisco de León");
            pl58 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352858342");
            PhoneLineEmployee ple58 = new PhoneLineEmployee() { EmployeeId = e58.EmployeeId, PhoneNumber = pl58.PhoneNumber };
            SMSPlanAssignment spa58 = new SMSPlanAssignment() { PhoneNumber = pl58.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple58);
            db.SmsPlanAssignments.Add(spa58);
            db.SaveChanges();
            //
            Employee e59 = new Employee() { Name = "Gabor Rojas Infante", CostCenterCode = "07" }; // 52175924
            PhoneLine pl59 = new PhoneLine() { PhoneNumber = "5352175924", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "4245901", PIN = 7115, PUK = 73231147 };
            db.Employees.Add(e59);
            db.PhoneLines.Add(pl59);
            db.SaveChanges();
            e59 = db.Employees.FirstOrDefault(m => m.Name == "Gabor Rojas Infante");
            pl59 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352175924");
            PhoneLineEmployee ple59 = new PhoneLineEmployee() { EmployeeId = e59.EmployeeId, PhoneNumber = pl59.PhoneNumber };
            SMSPlanAssignment spa59 = new SMSPlanAssignment() { PhoneNumber = pl59.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple59);
            db.SmsPlanAssignments.Add(spa59);
            db.SaveChanges();
            //
            Employee e60 = new Employee() { Name = "Garita Custodios Taller Rosello", CostCenterCode = "01" }; // 52093827
            PhoneLine pl60 = new PhoneLine() { PhoneNumber = "5352093827", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "4246006", PIN = 5298, PUK = 94112225 };
            db.Employees.Add(e60);
            db.PhoneLines.Add(pl60);
            db.SaveChanges();
            e60 = db.Employees.FirstOrDefault(m => m.Name == "Garita Custodios Taller Rosello");
            pl60 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352093827");
            PhoneLineEmployee ple60 = new PhoneLineEmployee() { EmployeeId = e60.EmployeeId, PhoneNumber = pl60.PhoneNumber };
            SMSPlanAssignment spa60 = new SMSPlanAssignment() { PhoneNumber = pl60.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple60);
            db.SmsPlanAssignments.Add(spa60);
            db.SaveChanges();
            //
            Employee e61 = new Employee() { Name = "George Barroso Perez", CostCenterCode = "16", PersonalCode = "640501" }; // 52852686
            PhoneLine pl61 = new PhoneLine() { PhoneNumber = "5352852686", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "254557", PIN = 8701, PUK = 90660054 };
            db.Employees.Add(e61);
            db.PhoneLines.Add(pl61);
            db.SaveChanges();
            e61 = db.Employees.FirstOrDefault(m => m.Name == "George Barroso Perez");
            pl61 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352852686");
            PhoneLineEmployee ple61 = new PhoneLineEmployee() { EmployeeId = e61.EmployeeId, PhoneNumber = pl61.PhoneNumber };
            SMSPlanAssignment spa61 = new SMSPlanAssignment() { PhoneNumber = pl61.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple61);
            db.SmsPlanAssignments.Add(spa61);
            db.SaveChanges();
            //
            Employee e62 = new Employee() { Name = "Gerardo Balbin", Email = "gerardo.balvin@mcvcomercial.cu", CostCenterCode = "19" }; // 52166179
            PhoneLine pl62 = new PhoneLine() { PhoneNumber = "5352166179", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "3385832", PIN = 15, PUK = 90492807 };
            db.Employees.Add(e62);
            db.PhoneLines.Add(pl62);
            db.SaveChanges();
            e62 = db.Employees.FirstOrDefault(m => m.Name == "Gerardo Balbin");
            pl62 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352166179");
            PhoneLineEmployee ple62 = new PhoneLineEmployee() { EmployeeId = e62.EmployeeId, PhoneNumber = pl62.PhoneNumber };
            SMSPlanAssignment spa62 = new SMSPlanAssignment() { PhoneNumber = pl62.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple62);
            db.SmsPlanAssignments.Add(spa62);
            db.SaveChanges();
            //
            Employee e63 = new Employee() { Name = "Guadalupe Benitez", Email = "guadalupe.benitez@mcvcomercial.cu", CostCenterCode = "36", Extension = "815" }; // 52175928
            PhoneLine pl63 = new PhoneLine() { PhoneNumber = "5352175928", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "4245901", PIN = 1416, PUK = 11123625 };
            db.Employees.Add(e63);
            db.PhoneLines.Add(pl63);
            db.SaveChanges();
            e63 = db.Employees.FirstOrDefault(m => m.Name == "Guadalupe Benitez");
            pl63 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352175928");
            PhoneLineEmployee ple63 = new PhoneLineEmployee() { EmployeeId = e63.EmployeeId, PhoneNumber = pl63.PhoneNumber };
            SMSPlanAssignment spa63 = new SMSPlanAssignment() { PhoneNumber = pl63.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple63);
            db.SmsPlanAssignments.Add(spa63);
            db.SaveChanges();
            //
            Employee e64 = new Employee() { Name = "Guido Orta Garcia", Email = "guido.orta@mcvcomercial.cu", CostCenterCode = "29", Extension = "823", PersonalCode = "216155" }; // 52166182
            PhoneLine pl64 = new PhoneLine() { PhoneNumber = "5352166182", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "3385832", PIN = 6507, PUK = 28775380 };
            db.Employees.Add(e64);
            db.PhoneLines.Add(pl64);
            db.SaveChanges();
            e64 = db.Employees.FirstOrDefault(m => m.Name == "Guido Orta Garcia");
            pl64 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352166182");
            PhoneLineEmployee ple64 = new PhoneLineEmployee() { EmployeeId = e64.EmployeeId, PhoneNumber = pl64.PhoneNumber };
            SMSPlanAssignment spa64 = new SMSPlanAssignment() { PhoneNumber = pl64.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple64);
            db.SmsPlanAssignments.Add(spa64);
            db.SaveChanges();
            //
            Employee e65 = new Employee() { Name = "Hector Delgado Montero", CostCenterCode = "32" }; // 52151921
            PhoneLine pl65 = new PhoneLine() { PhoneNumber = "5352151921", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "6425317", PIN = 5746, PUK = 66262376 };
            db.Employees.Add(e65);
            db.PhoneLines.Add(pl65);
            db.SaveChanges();
            e65 = db.Employees.FirstOrDefault(m => m.Name == "Hector Delgado Montero");
            pl65 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352151921");
            PhoneLineEmployee ple65 = new PhoneLineEmployee() { EmployeeId = e65.EmployeeId, PhoneNumber = pl65.PhoneNumber };
            SMSPlanAssignment spa65 = new SMSPlanAssignment() { PhoneNumber = pl65.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple65);
            db.SmsPlanAssignments.Add(spa65);
            db.SaveChanges();
            //
            Employee e66 = new Employee() { Name = "Hector Machado", Email = "taller.turismo@mcvcomercial.cu", CostCenterCode = "30", Extension = "835" }; // 59956585
            PhoneLine pl66 = new PhoneLine() { PhoneNumber = "5359956585", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "5480809", PIN = 4917, PUK = 14208611 };
            db.Employees.Add(e66);
            db.PhoneLines.Add(pl66);
            db.SaveChanges();
            e66 = db.Employees.FirstOrDefault(m => m.Name == "Hector Machado");
            pl66 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5359956585");
            PhoneLineEmployee ple66 = new PhoneLineEmployee() { EmployeeId = e66.EmployeeId, PhoneNumber = pl66.PhoneNumber };
            SMSPlanAssignment spa66 = new SMSPlanAssignment() { PhoneNumber = pl66.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple66);
            db.SmsPlanAssignments.Add(spa66);
            db.SaveChanges();
            //
            Employee e67 = new Employee() { Name = "Henry Montero Tamayo", Email = "henry.montero@mcvcomercial.cu", CostCenterCode = "19" }; // 52134594
            PhoneLine pl67 = new PhoneLine() { PhoneNumber = "5352134594", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "5480809", PIN = 6421, PUK = 78902703 };
            db.Employees.Add(e67);
            db.PhoneLines.Add(pl67);
            db.SaveChanges();
            e67 = db.Employees.FirstOrDefault(m => m.Name == "Henry Montero Tamayo");
            pl67 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352134594");
            PhoneLineEmployee ple67 = new PhoneLineEmployee() { EmployeeId = e67.EmployeeId, PhoneNumber = pl67.PhoneNumber };
            SMSPlanAssignment spa67 = new SMSPlanAssignment() { PhoneNumber = pl67.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple67);
            db.SmsPlanAssignments.Add(spa67);
            db.SaveChanges();
            //
            Employee e68 = new Employee() { Name = "Hermes Cutiño", CostCenterCode = "34" }; // 52134598
            PhoneLine pl68 = new PhoneLine() { PhoneNumber = "5352134598", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "5480809", PIN = 0, PUK = 42939202 };
            db.Employees.Add(e68);
            db.PhoneLines.Add(pl68);
            db.SaveChanges();
            e68 = db.Employees.FirstOrDefault(m => m.Name == "Hermes Cutiño");
            pl68 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352134598");
            PhoneLineEmployee ple68 = new PhoneLineEmployee() { EmployeeId = e68.EmployeeId, PhoneNumber = pl68.PhoneNumber };
            SMSPlanAssignment spa68 = new SMSPlanAssignment() { PhoneNumber = pl68.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple68);
            db.SmsPlanAssignments.Add(spa68);
            db.SaveChanges();
            //
            Employee e69 = new Employee() { Name = "Hiram Alberti", CostCenterCode = "32" }; // 52134597
            PhoneLine pl69 = new PhoneLine() { PhoneNumber = "5352134597", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "1457427", PIN = 1378, PUK = 19025770 };
            db.Employees.Add(e69);
            db.PhoneLines.Add(pl69);
            db.SaveChanges();
            e69 = db.Employees.FirstOrDefault(m => m.Name == "Hiram Alberti");
            pl69 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352134597");
            PhoneLineEmployee ple69 = new PhoneLineEmployee() { EmployeeId = e69.EmployeeId, PhoneNumber = pl69.PhoneNumber };
            SMSPlanAssignment spa69 = new SMSPlanAssignment() { PhoneNumber = pl69.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple69);
            db.SmsPlanAssignments.Add(spa69);
            db.SaveChanges();
            //
            Employee e70 = new Employee() { Name = "Ida Alonso", Email = "recepcion.taller@mcvcomercial.cu", CostCenterCode = "29", Extension = "825", PersonalCode = "690213" }; // 59969794
            PhoneLine pl70 = new PhoneLine() { PhoneNumber = "5359969794", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "3385832", PIN = 1162, PUK = 48177242 };
            db.Employees.Add(e70);
            db.PhoneLines.Add(pl70);
            db.SaveChanges();
            e70 = db.Employees.FirstOrDefault(m => m.Name == "Ida Alonso");
            pl70 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5359969794");
            PhoneLineEmployee ple70 = new PhoneLineEmployee() { EmployeeId = e70.EmployeeId, PhoneNumber = pl70.PhoneNumber };
            SMSPlanAssignment spa70 = new SMSPlanAssignment() { PhoneNumber = pl70.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = sp25SoloRec.SMSPlanId };
            db.PhoneLineEmployees.Add(ple70);
            db.SmsPlanAssignments.Add(spa70);
            db.SaveChanges();
            //
            Employee e71 = new Employee() { Name = "Ignacio A Borrero", CostCenterCode = "15" }; // 52166081
            PhoneLine pl71 = new PhoneLine() { PhoneNumber = "5352166081", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "3385832", PIN = 9825, PUK = 12405871 };
            db.Employees.Add(e71);
            db.PhoneLines.Add(pl71);
            db.SaveChanges();
            e71 = db.Employees.FirstOrDefault(m => m.Name == "Ignacio A Borrero");
            pl71 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352166081");
            PhoneLineEmployee ple71 = new PhoneLineEmployee() { EmployeeId = e71.EmployeeId, PhoneNumber = pl71.PhoneNumber };
            SMSPlanAssignment spa71 = new SMSPlanAssignment() { PhoneNumber = pl71.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple71);
            db.SmsPlanAssignments.Add(spa71);
            db.SaveChanges();
            //
            Employee e72 = new Employee() { Name = "Iohann Rafull", Email = "iohan.rafull@mcvcomercial.cu", CostCenterCode = "29", Extension = "805" }; // 52128667
            PhoneLine pl72 = new PhoneLine() { PhoneNumber = "5352128667", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "258800", PIN = 2222, PUK = 24861210 };
            db.Employees.Add(e72);
            db.PhoneLines.Add(pl72);
            db.SaveChanges();
            e72 = db.Employees.FirstOrDefault(m => m.Name == "Iohann Rafull");
            pl72 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352128667");
            PhoneLineEmployee ple72 = new PhoneLineEmployee() { EmployeeId = e72.EmployeeId, PhoneNumber = pl72.PhoneNumber };
            SMSPlanAssignment spa72 = new SMSPlanAssignment() { PhoneNumber = pl72.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple72);
            db.SmsPlanAssignments.Add(spa72);
            db.SaveChanges();
            //
            Employee e73 = new Employee() { Name = "Jesus Abdel Lahera", CostCenterCode = "06" }; // 52121432
            PhoneLine pl73 = new PhoneLine() { PhoneNumber = "5352121432", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "5399954", PIN = 1920, PUK = 17241811 };
            db.Employees.Add(e73);
            db.PhoneLines.Add(pl73);
            db.SaveChanges();
            e73 = db.Employees.FirstOrDefault(m => m.Name == "Jesus Abdel Lahera");
            pl73 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352121432");
            PhoneLineEmployee ple73 = new PhoneLineEmployee() { EmployeeId = e73.EmployeeId, PhoneNumber = pl73.PhoneNumber };
            SMSPlanAssignment spa73 = new SMSPlanAssignment() { PhoneNumber = pl73.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple73);
            db.SmsPlanAssignments.Add(spa73);
            db.SaveChanges();
            //
            Employee e74 = new Employee() { Name = "Jesus Pozo Torres", Email = "jusus.pozo@mcvcomercial.cu", CostCenterCode = "29", Extension = "822", PersonalCode = "291971" }; //52850461
            PhoneLine pl74 = new PhoneLine() { PhoneNumber = "5352850461", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "272145", PIN = 6905, PUK = 47209 };
            db.Employees.Add(e74);
            db.PhoneLines.Add(pl74);
            db.SaveChanges();
            e74 = db.Employees.FirstOrDefault(m => m.Name == "Jesus Pozo Torres");
            pl74 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352850461");
            PhoneLineEmployee ple74 = new PhoneLineEmployee() { EmployeeId = e74.EmployeeId, PhoneNumber = pl74.PhoneNumber };
            SMSPlanAssignment spa74 = new SMSPlanAssignment() { PhoneNumber = pl74.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple74);
            db.SmsPlanAssignments.Add(spa74);
            db.SaveChanges();
            //
            Employee e75 = new Employee() { Name = "Joel Hernandez Gonzalez", Email = "joel.hernandez@mcvcomercial.cu", CostCenterCode = "16", Extension = "759", PersonalCode = "131415" }; //52093819
            PhoneLine pl75 = new PhoneLine() { PhoneNumber = "5352093819", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "4245901", PIN = 1135, PUK = 28037867 };
            db.Employees.Add(e75);
            db.PhoneLines.Add(pl75);
            db.SaveChanges();
            e75 = db.Employees.FirstOrDefault(m => m.Name == "Joel Hernandez Gonzalez");
            pl75 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352093819");
            PhoneLineEmployee ple75 = new PhoneLineEmployee() { EmployeeId = e75.EmployeeId, PhoneNumber = pl75.PhoneNumber };
            SMSPlanAssignment spa75 = new SMSPlanAssignment() { PhoneNumber = pl75.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple75);
            db.SmsPlanAssignments.Add(spa75);
            db.SaveChanges();
            //
            Employee e76 = new Employee() { Name = "Jorge Fonticoba", Email = "jorge.fonticoba@mcvcomercial.cu", CostCenterCode = "29", Extension = "803" }; //52748787
            PhoneLine pl76 = new PhoneLine() { PhoneNumber = "5352748787", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "5480809", PIN = 5933, PUK = 28353041 };
            db.Employees.Add(e76);
            db.PhoneLines.Add(pl76);
            db.SaveChanges();
            e76 = db.Employees.FirstOrDefault(m => m.Name == "Jorge Fonticoba");
            pl76 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352748787");
            PhoneLineEmployee ple76 = new PhoneLineEmployee() { EmployeeId = e76.EmployeeId, PhoneNumber = pl76.PhoneNumber };
            SMSPlanAssignment spa76 = new SMSPlanAssignment() { PhoneNumber = pl76.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple76);
            db.SmsPlanAssignments.Add(spa76);
            db.SaveChanges();
            //
            Employee e77 = new Employee() { Name = "Jorge Ricardo Oliva", CostCenterCode = "20" }; //52143738
            PhoneLine pl77 = new PhoneLine() { PhoneNumber = "5352143738", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "754172", PIN = 817, PUK = 54868439 };
            db.Employees.Add(e77);
            db.PhoneLines.Add(pl77);
            db.SaveChanges();
            e77 = db.Employees.FirstOrDefault(m => m.Name == "Jorge Ricardo Oliva");
            pl77 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352143738");
            PhoneLineEmployee ple77 = new PhoneLineEmployee() { EmployeeId = e77.EmployeeId, PhoneNumber = pl77.PhoneNumber };
            SMSPlanAssignment spa77 = new SMSPlanAssignment() { PhoneNumber = pl77.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple77);
            db.SmsPlanAssignments.Add(spa77);
            db.SaveChanges();
            //
            Employee e78 = new Employee() { Name = "Jose A Gonzalez Mtnez", Email = "jose.gonzalez@mcvcomercial.cu", CostCenterCode = "29" }; //52134601
            PhoneLine pl78 = new PhoneLine() { PhoneNumber = "5352134601", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "5480809", PIN = 6786, PUK = 42770692 };
            db.Employees.Add(e78);
            db.PhoneLines.Add(pl78);
            db.SaveChanges();
            e78 = db.Employees.FirstOrDefault(m => m.Name == "Jose A Gonzalez Mtnez");
            pl78 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352134601");
            PhoneLineEmployee ple78 = new PhoneLineEmployee() { EmployeeId = e78.EmployeeId, PhoneNumber = pl78.PhoneNumber };
            SMSPlanAssignment spa78 = new SMSPlanAssignment() { PhoneNumber = pl78.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple78);
            db.SmsPlanAssignments.Add(spa78);
            db.SaveChanges();
            //
            Employee e79 = new Employee() { Name = "Jose Alvarez Tamayo", Email = "jose.alvarez@mcvcomercial.cu", CostCenterCode = "02", Extension = "724", PersonalCode = "010218" }; //52799333
            PhoneLine pl79 = new PhoneLine() { PhoneNumber = "5352799333", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "3385832", PIN = 5703, PUK = 79076279 };
            db.Employees.Add(e79);
            db.PhoneLines.Add(pl79);
            db.SaveChanges();
            e79 = db.Employees.FirstOrDefault(m => m.Name == "Jose Alvarez Tamayo");
            pl79 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352799333");
            PhoneLineEmployee ple79 = new PhoneLineEmployee() { EmployeeId = e79.EmployeeId, PhoneNumber = pl79.PhoneNumber };
            SMSPlanAssignment spa79 = new SMSPlanAssignment() { PhoneNumber = pl79.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple79);
            db.SmsPlanAssignments.Add(spa79);
            db.SaveChanges();
            //
            Employee e80 = new Employee() { Name = "Jose Antonio González", CostCenterCode = "04", Extension = "875", PersonalCode = "921695" }; //52801768
            PhoneLine pl80 = new PhoneLine() { PhoneNumber = "5352801768", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "357902", PIN = 4750, PUK = 34886961 };
            db.Employees.Add(e80);
            db.PhoneLines.Add(pl80);
            db.SaveChanges();
            e80 = db.Employees.FirstOrDefault(m => m.Name == "Jose Antonio González");
            pl80 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352801768");
            PhoneLineEmployee ple80 = new PhoneLineEmployee() { EmployeeId = e80.EmployeeId, PhoneNumber = pl80.PhoneNumber };
            SMSPlanAssignment spa80 = new SMSPlanAssignment() { PhoneNumber = pl80.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple80);
            db.SmsPlanAssignments.Add(spa80);
            db.SaveChanges();
            //
            Employee e81 = new Employee() { Name = "Jose Fernández", Email = "jose.fernandez@mcvcomercial.cu", CostCenterCode = "07", Extension = "613" }; //52873174
            PhoneLine pl81 = new PhoneLine() { PhoneNumber = "5352873174", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "429536", PIN = 3488, PUK = 56885210 };
            db.Employees.Add(e81);
            db.PhoneLines.Add(pl81);
            db.SaveChanges();
            e81 = db.Employees.FirstOrDefault(m => m.Name == "Jose Fernández");
            pl81 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352873174");
            PhoneLineEmployee ple81 = new PhoneLineEmployee() { EmployeeId = e81.EmployeeId, PhoneNumber = pl81.PhoneNumber };
            SMSPlanAssignment spa81 = new SMSPlanAssignment() { PhoneNumber = pl81.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = sp25SoloRec.SMSPlanId };
            db.PhoneLineEmployees.Add(ple81);
            db.SmsPlanAssignments.Add(spa81);
            db.SaveChanges();
            //
            Employee e82 = new Employee() { Name = "Jose Garcia Perez", Email = "jose.garcia@mcvcomercial.cu", CostCenterCode = "01" }; //52151917
            PhoneLine pl82 = new PhoneLine() { PhoneNumber = "5352151917", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "6425317", PIN = 848, PUK = 36269768 };
            db.Employees.Add(e82);
            db.PhoneLines.Add(pl82);
            db.SaveChanges();
            e82 = db.Employees.FirstOrDefault(m => m.Name == "Jose Garcia Perez");
            pl82 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352151917");
            PhoneLineEmployee ple82 = new PhoneLineEmployee() { EmployeeId = e82.EmployeeId, PhoneNumber = pl82.PhoneNumber };
            SMSPlanAssignment spa82 = new SMSPlanAssignment() { PhoneNumber = pl82.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple82);
            db.SmsPlanAssignments.Add(spa82);
            db.SaveChanges();
            //
            Employee e83 = new Employee() { Name = "Jose Jesus Ramos Fonte", Email = "jesus.ramos@mcvcomercial.cu", CostCenterCode = "19" }; //52850845
            PhoneLine pl83 = new PhoneLine() { PhoneNumber = "5352850845", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "1528685", PIN = 3711, PUK = 15179137 };
            db.Employees.Add(e83);
            db.PhoneLines.Add(pl83);
            db.SaveChanges();
            e83 = db.Employees.FirstOrDefault(m => m.Name == "Jose Jesus Ramos Fonte");
            pl83 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352850845");
            PhoneLineEmployee ple83 = new PhoneLineEmployee() { EmployeeId = e83.EmployeeId, PhoneNumber = pl83.PhoneNumber };
            SMSPlanAssignment spa83 = new SMSPlanAssignment() { PhoneNumber = pl83.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple83);
            db.SmsPlanAssignments.Add(spa83);
            db.SaveChanges();
            //
            Employee e84 = new Employee() { Name = "Jose Miguel Marquez Blanco", Email = "jose.marquez@mcvcomercial.cu", CostCenterCode = "20", Extension = "889", PersonalCode = "746292" }; //52093824
            PhoneLine pl84 = new PhoneLine() { PhoneNumber = "5352093824", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "4245922", PIN = 4890, PUK = 29966929 };
            db.Employees.Add(e84);
            db.PhoneLines.Add(pl84);
            db.SaveChanges();
            e84 = db.Employees.FirstOrDefault(m => m.Name == "Jose Miguel Marquez Blanco");
            pl84 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352093824");
            PhoneLineEmployee ple84 = new PhoneLineEmployee() { EmployeeId = e84.EmployeeId, PhoneNumber = pl84.PhoneNumber };
            SMSPlanAssignment spa84 = new SMSPlanAssignment() { PhoneNumber = pl84.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple84);
            db.SmsPlanAssignments.Add(spa84);
            db.SaveChanges();
            //
            Employee e85 = new Employee() { Name = "Jose Viera", Email = "jose.viera@mcvcomercial.cu", CostCenterCode = "19", Extension = "603" }; //52803741
            PhoneLine pl85 = new PhoneLine() { PhoneNumber = "5352803741", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "272371", PIN = 4968, PUK = 78764109 };
            db.Employees.Add(e85);
            db.PhoneLines.Add(pl85);
            db.SaveChanges();
            e85 = db.Employees.FirstOrDefault(m => m.Name == "Jose Viera");
            pl85 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352803741");
            PhoneLineEmployee ple85 = new PhoneLineEmployee() { EmployeeId = e85.EmployeeId, PhoneNumber = pl85.PhoneNumber };
            SMSPlanAssignment spa85 = new SMSPlanAssignment() { PhoneNumber = pl85.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple85);
            db.SmsPlanAssignments.Add(spa85);
            db.SaveChanges();
            //
            Employee e86 = new Employee() { Name = "Juan Alberto Rodriguez", Email = "juan.rodriguez@mcvcomercial.cu", CostCenterCode = "29", PersonalCode = "110216" }; //52112630
            PhoneLine pl86 = new PhoneLine() { PhoneNumber = "5352112630", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "4926540", PIN = 653, PUK = 56222023 };
            db.Employees.Add(e86);
            db.PhoneLines.Add(pl86);
            db.SaveChanges();
            e86 = db.Employees.FirstOrDefault(m => m.Name == "Juan Alberto Rodriguez");
            pl86 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352112630");
            PhoneLineEmployee ple86 = new PhoneLineEmployee() { EmployeeId = e86.EmployeeId, PhoneNumber = pl86.PhoneNumber };
            SMSPlanAssignment spa86 = new SMSPlanAssignment() { PhoneNumber = pl86.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple86);
            db.SmsPlanAssignments.Add(spa86);
            db.SaveChanges();
            //
            Employee e87 = new Employee() { Name = "Juan Carlos Portela", Email = "juan.portela@mcvcomercial.cu", CostCenterCode = "29", Extension = "832", PersonalCode = "150930" }; //52852685
            PhoneLine pl87 = new PhoneLine() { PhoneNumber = "5352852685", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "254553", PIN = 1140, PUK = 6074817 };
            db.Employees.Add(e87);
            db.PhoneLines.Add(pl87);
            db.SaveChanges();
            e87 = db.Employees.FirstOrDefault(m => m.Name == "Juan Carlos Portela");
            pl87 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352852685");
            PhoneLineEmployee ple87 = new PhoneLineEmployee() { EmployeeId = e87.EmployeeId, PhoneNumber = pl87.PhoneNumber };
            SMSPlanAssignment spa87 = new SMSPlanAssignment() { PhoneNumber = pl87.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple87);
            db.SmsPlanAssignments.Add(spa87);
            db.SaveChanges();
            //
            Employee e88 = new Employee() { Name = "Juan Carlos Reyes", Email = "juan.reyes@mcvcomercial.cu", CostCenterCode = "01", Extension = "746" }; //52097350
            PhoneLine pl88 = new PhoneLine() { PhoneNumber = "5352097350", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "4317919", PIN = 4556, PUK = 13471855 };
            db.Employees.Add(e88);
            db.PhoneLines.Add(pl88);
            db.SaveChanges();
            e88 = db.Employees.FirstOrDefault(m => m.Name == "Juan Carlos Reyes");
            pl88 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352097350");
            PhoneLineEmployee ple88 = new PhoneLineEmployee() { EmployeeId = e88.EmployeeId, PhoneNumber = pl88.PhoneNumber };
            SMSPlanAssignment spa88 = new SMSPlanAssignment() { PhoneNumber = pl88.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple88);
            db.SmsPlanAssignments.Add(spa88);
            db.SaveChanges();
            //
            Employee e89 = new Employee() { Name = "Juan Corrales", CostCenterCode = "29", PersonalCode = "271016" }; //52806432
            PhoneLine pl89 = new PhoneLine() { PhoneNumber = "5352806432", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "2073052", PIN = 2900, PUK = 11560217 };
            db.Employees.Add(e89);
            db.PhoneLines.Add(pl89);
            db.SaveChanges();
            e89 = db.Employees.FirstOrDefault(m => m.Name == "Juan Corrales");
            pl89 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352806432");
            PhoneLineEmployee ple89 = new PhoneLineEmployee() { EmployeeId = e89.EmployeeId, PhoneNumber = pl89.PhoneNumber };
            SMSPlanAssignment spa89 = new SMSPlanAssignment() { PhoneNumber = pl89.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple89);
            db.SmsPlanAssignments.Add(spa89);
            db.SaveChanges();
            //
            Employee e90 = new Employee() { Name = "Juan Bautista", Email = "juan.bautista@mcvcomercial.cu", CostCenterCode = "29", Extension = "819", PersonalCode = "270654" }; //52134593
            PhoneLine pl90 = new PhoneLine() { PhoneNumber = "5352134593", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "275908", PIN = 1954, PUK = 70359151 };
            db.Employees.Add(e90);
            db.PhoneLines.Add(pl90);
            db.SaveChanges();
            e90 = db.Employees.FirstOrDefault(m => m.Name == "Juan Bautista");
            pl90 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352134593");
            PhoneLineEmployee ple90 = new PhoneLineEmployee() { EmployeeId = e90.EmployeeId, PhoneNumber = pl90.PhoneNumber };
            SMSPlanAssignment spa90 = new SMSPlanAssignment() { PhoneNumber = pl90.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple90);
            db.SmsPlanAssignments.Add(spa90);
            db.SaveChanges();
            //
            Employee e91 = new Employee() { Name = "Julio Pupo", Email = "julio.pupo@mcvcomercial.cu", CostCenterCode = "20", Extension = "841", PersonalCode = "661921" }; //52855777
            PhoneLine pl91 = new PhoneLine() { PhoneNumber = "5352855777", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "336139", PIN = 1654, PUK = 55541884 };
            db.Employees.Add(e91);
            db.PhoneLines.Add(pl91);
            db.SaveChanges();
            e91 = db.Employees.FirstOrDefault(m => m.Name == "Julio Pupo");
            pl91 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352855777");
            PhoneLineEmployee ple91 = new PhoneLineEmployee() { EmployeeId = e91.EmployeeId, PhoneNumber = pl91.PhoneNumber };
            SMSPlanAssignment spa91 = new SMSPlanAssignment() { PhoneNumber = pl91.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple91);
            db.SmsPlanAssignments.Add(spa91);
            db.SaveChanges();
            //
            Employee e92 = new Employee() { Name = "Justo Ramirez Vaillant", CostCenterCode = "01" }; //52804769
            PhoneLine pl92 = new PhoneLine() { PhoneNumber = "5352804769", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "1457427", PIN = 1378, PUK = 19025770 };
            db.Employees.Add(e92);
            db.PhoneLines.Add(pl92);
            db.SaveChanges();
            e92 = db.Employees.FirstOrDefault(m => m.Name == "Justo Ramirez Vaillant");
            pl92 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352804769");
            PhoneLineEmployee ple92 = new PhoneLineEmployee() { EmployeeId = e92.EmployeeId, PhoneNumber = pl92.PhoneNumber };
            SMSPlanAssignment spa92 = new SMSPlanAssignment() { PhoneNumber = pl92.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple92);
            db.SmsPlanAssignments.Add(spa92);
            db.SaveChanges();
            //
            Employee e93 = new Employee() { Name = "Karim Ghabbour", CostCenterCode = "01", Extension = "793" }; //52869591
            PhoneLine pl93 = new PhoneLine() { PhoneNumber = "5352869591", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "260194", PIN = 4183, PUK = 57284447 };
            db.Employees.Add(e93);
            db.PhoneLines.Add(pl93);
            db.SaveChanges();
            e93 = db.Employees.FirstOrDefault(m => m.Name == "Karim Ghabbour");
            pl93 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352869591");
            PhoneLineEmployee ple93 = new PhoneLineEmployee() { EmployeeId = e93.EmployeeId, PhoneNumber = pl93.PhoneNumber };
            SMSPlanAssignment spa93 = new SMSPlanAssignment() { PhoneNumber = pl93.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple93);
            db.SmsPlanAssignments.Add(spa93);
            db.SaveChanges();
            //
            Employee e94 = new Employee() { Name = "Lazaro Castro", Email = "lazaro.castro@mcvcomercial.cu", CostCenterCode = "02", Extension = "725", PersonalCode = "220488" }; //52859348
            PhoneLine pl94 = new PhoneLine() { PhoneNumber = "5352859348", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "4188941", PIN = 9649, PUK = 38649785 };
            db.Employees.Add(e94);
            db.PhoneLines.Add(pl94);
            db.SaveChanges();
            e94 = db.Employees.FirstOrDefault(m => m.Name == "Lazaro Castro");
            pl94 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352859348");
            PhoneLineEmployee ple94 = new PhoneLineEmployee() { EmployeeId = e94.EmployeeId, PhoneNumber = pl94.PhoneNumber };
            SMSPlanAssignment spa94 = new SMSPlanAssignment() { PhoneNumber = pl94.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple94);
            db.SmsPlanAssignments.Add(spa94);
            db.SaveChanges();
            //
            Employee e95 = new Employee() { Name = "Lazaro Díaz", Email = "lazaro.diaz@mcvcomercial.cu", CostCenterCode = "05", Extension = "716", PersonalCode = "865882" }; //52875916
            PhoneLine pl95 = new PhoneLine() { PhoneNumber = "5352875916", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "670868", PIN = 4693, PUK = 16212459 };
            db.Employees.Add(e95);
            db.PhoneLines.Add(pl95);
            db.SaveChanges();
            e95 = db.Employees.FirstOrDefault(m => m.Name == "Lazaro Díaz");
            pl95 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352875916");
            PhoneLineEmployee ple95 = new PhoneLineEmployee() { EmployeeId = e95.EmployeeId, PhoneNumber = pl95.PhoneNumber };
            SMSPlanAssignment spa95 = new SMSPlanAssignment() { PhoneNumber = pl95.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = sp25SoloRec.SMSPlanId };
            db.PhoneLineEmployees.Add(ple95);
            db.SmsPlanAssignments.Add(spa95);
            db.SaveChanges();
            //
            Employee e96 = new Employee() { Name = "Lazaro Rodriguez Alvarez", Email = "lazaro.rodriguez@mcvcomercial.cu", CostCenterCode = "19" }; //52631273
            PhoneLine pl96 = new PhoneLine() { PhoneNumber = "5352631273", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "225461", PIN = 792, PUK = 6824467 };
            db.Employees.Add(e96);
            db.PhoneLines.Add(pl96);
            db.SaveChanges();
            e96 = db.Employees.FirstOrDefault(m => m.Name == "Lazaro Rodriguez Alvarez");
            pl96 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352631273");
            PhoneLineEmployee ple96 = new PhoneLineEmployee() { EmployeeId = e96.EmployeeId, PhoneNumber = pl96.PhoneNumber };
            SMSPlanAssignment spa96 = new SMSPlanAssignment() { PhoneNumber = pl96.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple96);
            db.SmsPlanAssignments.Add(spa96);
            db.SaveChanges();
            //
            Employee e97 = new Employee() { Name = "Leocadio Sanchez", CostCenterCode = "33" }; // 52151919
            PhoneLine pl97 = new PhoneLine() { PhoneNumber = "5352151919", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "6425317", PIN = 2333, PUK = 4458693 };
            db.Employees.Add(e97);
            db.PhoneLines.Add(pl97);
            db.SaveChanges();
            e97 = db.Employees.FirstOrDefault(m => m.Name == "Leocadio Sanchez");
            pl97 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352151919");
            PhoneLineEmployee ple97 = new PhoneLineEmployee() { EmployeeId = e97.EmployeeId, PhoneNumber = pl97.PhoneNumber };
            SMSPlanAssignment spa97 = new SMSPlanAssignment() { PhoneNumber = pl97.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple97);
            db.SmsPlanAssignments.Add(spa97);
            db.SaveChanges();
            //
            Employee e98 = new Employee() { Name = "Leonid Leiva Mayor", Email = "leonid.leiva@mcvcomercial.cu", CostCenterCode = "35", Extension = "873", PersonalCode = "051217" }; //52121787
            PhoneLine pl98 = new PhoneLine() { PhoneNumber = "5352121787", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "5480809", PIN = 121, PUK = 78871395 };
            db.Employees.Add(e98);
            db.PhoneLines.Add(pl98);
            db.SaveChanges();
            e98 = db.Employees.FirstOrDefault(m => m.Name == "Leonid Leiva Mayor");
            pl98 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352121787");
            PhoneLineEmployee ple98 = new PhoneLineEmployee() { EmployeeId = e98.EmployeeId, PhoneNumber = pl98.PhoneNumber };
            SMSPlanAssignment spa98 = new SMSPlanAssignment() { PhoneNumber = pl98.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple98);
            db.SmsPlanAssignments.Add(spa98);
            db.SaveChanges();
            //
            Employee e99 = new Employee() { Name = "Leydi Lasso", Email = "leydi.lasso@mcvcomercial.cu", CostCenterCode = "05", Extension = "624", PersonalCode = "330883" }; //52865888
            PhoneLine pl99 = new PhoneLine() { PhoneNumber = "5352865888", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "258803", PIN = 0722, PUK = 65284870 };
            db.Employees.Add(e99);
            db.PhoneLines.Add(pl99);
            db.SaveChanges();
            e99 = db.Employees.FirstOrDefault(m => m.Name == "Leydi Lasso");
            pl99 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352865888");
            PhoneLineEmployee ple99 = new PhoneLineEmployee() { EmployeeId = e99.EmployeeId, PhoneNumber = pl99.PhoneNumber };
            SMSPlanAssignment spa99 = new SMSPlanAssignment() { PhoneNumber = pl99.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple99);
            db.SmsPlanAssignments.Add(spa99);
            db.SaveChanges();
            //
            Employee e100 = new Employee() { Name = "Luis Alberto Lemus Hdez", Email = "luis.lemus@mcvcomercial.cu", CostCenterCode = "15", Extension = "516" }; // 52866939 
            PhoneLine pl100 = new PhoneLine() { PhoneNumber = "5352866939", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "258803", PIN = 350, PUK = 64532314 };
            db.Employees.Add(e100);
            db.PhoneLines.Add(pl100);
            db.SaveChanges();
            e100 = db.Employees.FirstOrDefault(m => m.Name == "Luis Alberto Lemus Hdez");
            pl100 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352866939");
            PhoneLineEmployee ple100 = new PhoneLineEmployee() { EmployeeId = e100.EmployeeId, PhoneNumber = pl100.PhoneNumber };
            SMSPlanAssignment spa100 = new SMSPlanAssignment() { PhoneNumber = pl100.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple100);
            db.SmsPlanAssignments.Add(spa100);
            db.SaveChanges();
            //
            Employee e101 = new Employee() { Name = "Luis Alberto Rodriguez", Email = "luis.rodriguez@mcvcomercial.cu", CostCenterCode = "15", Extension = "516" }; // 52872580
            PhoneLine pl101 = new PhoneLine() { PhoneNumber = "5352872580", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "772384", PIN = 6149, PUK = 61294097 };
            db.Employees.Add(e101);
            db.PhoneLines.Add(pl101);
            db.SaveChanges();
            e101 = db.Employees.FirstOrDefault(m => m.Name == "Luis Alberto Rodriguez");
            pl101 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352872580");
            PhoneLineEmployee ple101 = new PhoneLineEmployee() { EmployeeId = e101.EmployeeId, PhoneNumber = pl101.PhoneNumber, };
            SMSPlanAssignment spa101 = new SMSPlanAssignment() { PhoneNumber = pl101.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = sp25SoloRec.SMSPlanId };
            db.PhoneLineEmployees.Add(ple101);
            db.SmsPlanAssignments.Add(spa101);
            db.SaveChanges();
            //
            Employee e102 = new Employee() { Name = "Maday Saez Molina", Email = "maday.saez@mcvcomercial.cu", CostCenterCode = "01", Extension = "820", PersonalCode = "240815" }; //52160876
            PhoneLine pl102 = new PhoneLine() { PhoneNumber = "5352160876", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "3385832", PIN = 9576, PUK = 47712936 };
            db.Employees.Add(e102);
            db.PhoneLines.Add(pl102);
            db.SaveChanges();
            e102 = db.Employees.FirstOrDefault(m => m.Name == "Maday Saez Molina");
            pl102 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352160876");
            PhoneLineEmployee ple102 = new PhoneLineEmployee() { EmployeeId = e102.EmployeeId, PhoneNumber = pl102.PhoneNumber };
            SMSPlanAssignment spa102 = new SMSPlanAssignment() { PhoneNumber = pl102.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = sp25SoloRec.SMSPlanId };
            db.PhoneLineEmployees.Add(ple102);
            db.SmsPlanAssignments.Add(spa102);
            db.SaveChanges();
            //
            Employee e103 = new Employee() { Name = "Maikel Machado Martínez", Email = "maikel.machado@mcvcomercial.cu", CostCenterCode = "19", Extension = "607" }; //52805535
            PhoneLine pl103 = new PhoneLine() { PhoneNumber = "5352805535", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "1528685", PIN = 4189, PUK = 8716003 };
            db.Employees.Add(e103);
            db.PhoneLines.Add(pl103);
            db.SaveChanges();
            e103 = db.Employees.FirstOrDefault(m => m.Name == "Maikel Machado Martínez");
            pl103 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352805535");
            PhoneLineEmployee ple103 = new PhoneLineEmployee() { EmployeeId = e103.EmployeeId, PhoneNumber = pl103.PhoneNumber };
            SMSPlanAssignment spa103 = new SMSPlanAssignment() { PhoneNumber = pl103.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple103);
            db.SmsPlanAssignments.Add(spa103);
            db.SaveChanges();
            //
            Employee e104 = new Employee() { Name = "Manuel Torres Perez", Email = "manuel.torres@mcvcomercial.cu", CostCenterCode = "16", Extension = "757", PersonalCode = "070717" }; //52631453
            PhoneLine pl104 = new PhoneLine() { PhoneNumber = "5352631453", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "232098", PIN = 4554, PUK = 10358514 };
            db.Employees.Add(e104);
            db.PhoneLines.Add(pl104);
            db.SaveChanges();
            e104 = db.Employees.FirstOrDefault(m => m.Name == "Manuel Torres Perez");
            pl104 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352631453");
            PhoneLineEmployee ple104 = new PhoneLineEmployee() { EmployeeId = e104.EmployeeId, PhoneNumber = pl104.PhoneNumber };
            SMSPlanAssignment spa104 = new SMSPlanAssignment() { PhoneNumber = pl104.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple104);
            db.SmsPlanAssignments.Add(spa104);
            db.SaveChanges();
            //
            Employee e105 = new Employee() { Name = "Manuel Varela Valdes", CostCenterCode = "19" }; // 52163922
            PhoneLine pl105 = new PhoneLine() { PhoneNumber = "5352163922", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "5399954", PIN = 8752, PUK = 54577424 };
            db.Employees.Add(e105);
            db.PhoneLines.Add(pl105);
            db.SaveChanges();
            e105 = db.Employees.FirstOrDefault(m => m.Name == "Manuel Varela Valdes");
            pl105 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352163922");
            PhoneLineEmployee ple105 = new PhoneLineEmployee() { EmployeeId = e105.EmployeeId, PhoneNumber = pl105.PhoneNumber };
            SMSPlanAssignment spa105 = new SMSPlanAssignment() { PhoneNumber = pl105.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple105);
            db.SmsPlanAssignments.Add(spa105);
            db.SaveChanges();
            //
            Employee e106 = new Employee() { Name = "Maria Aleida Del Riego", Email = "manuel.delriego@mcvcomercial.cu", CostCenterCode = "01", Extension = "791", PersonalCode = "710509" }; //52112633
            PhoneLine pl106 = new PhoneLine() { PhoneNumber = "5352112633", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "4926540", PIN = 8343, PUK = 46891614 };
            db.Employees.Add(e106);
            db.PhoneLines.Add(pl106);
            db.SaveChanges();
            e106 = db.Employees.FirstOrDefault(m => m.Name == "Maria Aleida Del Riego");
            pl106 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352112633");
            PhoneLineEmployee ple106 = new PhoneLineEmployee() { EmployeeId = e106.EmployeeId, PhoneNumber = pl106.PhoneNumber };
            SMSPlanAssignment spa106 = new SMSPlanAssignment() { PhoneNumber = pl106.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple106);
            db.SmsPlanAssignments.Add(spa106);
            db.SaveChanges();
            //
            Employee e107 = new Employee() { Name = "Marta González", Email = "marta.gonzalez@mcvcomercial.cu", CostCenterCode = "01", Extension = "794", PersonalCode = "252525" }; //52852687
            PhoneLine pl107 = new PhoneLine() { PhoneNumber = "5352852687", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "254559", PIN = 9002, PUK = 47190856 };
            db.Employees.Add(e107);
            db.PhoneLines.Add(pl107);
            db.SaveChanges();
            e107 = db.Employees.FirstOrDefault(m => m.Name == "Marta González");
            pl107 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352852687");
            PhoneLineEmployee ple107 = new PhoneLineEmployee() { EmployeeId = e107.EmployeeId, PhoneNumber = pl107.PhoneNumber };
            SMSPlanAssignment spa107 = new SMSPlanAssignment() { PhoneNumber = pl107.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple107);
            db.SmsPlanAssignments.Add(spa107);
            db.SaveChanges();
            //
            Employee e108 = new Employee() { Name = "Maydene Novo Zarut", Email = "maydene.novo@mcvcomercial.cu", CostCenterCode = "20", Extension = "885", PersonalCode = "303132" }; //59969802
            PhoneLine pl108 = new PhoneLine() { PhoneNumber = "5359969802", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "3385832", PIN = 6022, PUK = 68121138 };
            db.Employees.Add(e108);
            db.PhoneLines.Add(pl108);
            db.SaveChanges();
            e108 = db.Employees.FirstOrDefault(m => m.Name == "Maydene Novo Zarut");
            pl108 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5359969802");
            PhoneLineEmployee ple108 = new PhoneLineEmployee() { EmployeeId = e108.EmployeeId, PhoneNumber = pl108.PhoneNumber };
            SMSPlanAssignment spa108 = new SMSPlanAssignment() { PhoneNumber = pl108.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple108);
            db.SmsPlanAssignments.Add(spa108);
            db.SaveChanges();
            //
            Employee e109 = new Employee() { Name = "Michel Zayas", CostCenterCode = "06" }; // 52148151
            PhoneLine pl109 = new PhoneLine() { PhoneNumber = "5352148151", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "6240174", PIN = 1775, PUK = 34169253 };
            db.Employees.Add(e109);
            db.PhoneLines.Add(pl109);
            db.SaveChanges();
            e109 = db.Employees.FirstOrDefault(m => m.Name == "Michel Zayas");
            pl109 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352148151");
            PhoneLineEmployee ple109 = new PhoneLineEmployee() { EmployeeId = e109.EmployeeId, PhoneNumber = pl109.PhoneNumber };
            SMSPlanAssignment spa109 = new SMSPlanAssignment() { PhoneNumber = pl109.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple109);
            db.SmsPlanAssignments.Add(spa109);
            db.SaveChanges();
            //
            Employee e110 = new Employee() { Name = "Miguel A Morales", Email = "miguel.morales@mcvcomercial.cu", CostCenterCode = "19" }; // 52166184
            PhoneLine pl110 = new PhoneLine() { PhoneNumber = "5352166184", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "3385832", PIN = 4327, PUK = 99212859 };
            db.Employees.Add(e110);
            db.PhoneLines.Add(pl110);
            db.SaveChanges();
            e110 = db.Employees.FirstOrDefault(m => m.Name == "Miguel A Morales");
            pl110 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352166184");
            PhoneLineEmployee ple110 = new PhoneLineEmployee() { EmployeeId = e110.EmployeeId, PhoneNumber = pl110.PhoneNumber };
            SMSPlanAssignment spa110 = new SMSPlanAssignment() { PhoneNumber = pl110.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple110);
            db.SmsPlanAssignments.Add(spa110);
            db.SaveChanges();
            //
            Employee e111 = new Employee() { Name = "Miguel Guerrero", Email = "miguel.guerrero@mcvcomercial.cu", CostCenterCode = "30", Extension = "834", PersonalCode = "200218" }; //52151915
            PhoneLine pl111 = new PhoneLine() { PhoneNumber = "5352151915", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "6425317", PIN = 6407, PUK = 84174219 };
            db.Employees.Add(e111);
            db.PhoneLines.Add(pl111);
            db.SaveChanges();
            e111 = db.Employees.FirstOrDefault(m => m.Name == "Miguel Guerrero");
            pl111 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352151915");
            PhoneLineEmployee ple111 = new PhoneLineEmployee() { EmployeeId = e111.EmployeeId, PhoneNumber = pl111.PhoneNumber };
            SMSPlanAssignment spa111 = new SMSPlanAssignment() { PhoneNumber = pl111.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple111);
            db.SmsPlanAssignments.Add(spa111);
            db.SaveChanges();
            //
            Employee e112 = new Employee() { Name = "Mijail Hernan Hernandez Miranda", Email = "mijail.hernandez@mcvcomercial.cu", CostCenterCode = "36", Extension = "817", PersonalCode = "301116" }; //52808515
            PhoneLine pl112 = new PhoneLine() { PhoneNumber = "5352808515", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "3568886", PIN = 9783, PUK = 68427307 };
            db.Employees.Add(e112);
            db.PhoneLines.Add(pl112);
            db.SaveChanges();
            e112 = db.Employees.FirstOrDefault(m => m.Name == "Mijail Hernan Hernandez Miranda");
            pl112 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352808515");
            PhoneLineEmployee ple112 = new PhoneLineEmployee() { EmployeeId = e112.EmployeeId, PhoneNumber = pl112.PhoneNumber };
            SMSPlanAssignment spa112 = new SMSPlanAssignment() { PhoneNumber = pl112.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple112);
            db.SmsPlanAssignments.Add(spa112);
            db.SaveChanges();
            //
            Employee e113 = new Employee() { Name = "Naury hechavarria", Email = "naury.hechavarria@mcvcomercial.cu", CostCenterCode = "19" }; // 52166177
            PhoneLine pl113 = new PhoneLine() { PhoneNumber = "5352166177", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "3385832", PIN = 4486, PUK = 52671892 };
            db.Employees.Add(e113);
            db.PhoneLines.Add(pl113);
            db.SaveChanges();
            e113 = db.Employees.FirstOrDefault(m => m.Name == "Naury Hechavarria");
            pl113 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352166177");
            PhoneLineEmployee ple113 = new PhoneLineEmployee() { EmployeeId = e113.EmployeeId, PhoneNumber = pl113.PhoneNumber };
            SMSPlanAssignment spa113 = new SMSPlanAssignment() { PhoneNumber = pl113.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple113);
            db.SmsPlanAssignments.Add(spa113);
            db.SaveChanges();
            //
            Employee e114 = new Employee() { Name = "Octavio Rodriguez Valdes", Email = "octavio.rodriguez@mcvcomercial.cu", CostCenterCode = "29", Extension = "897", PersonalCode = "300617" }; //52166183
            PhoneLine pl114 = new PhoneLine() { PhoneNumber = "5352166183", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "3385832", PIN = 4705, PUK = 72790539 };
            db.Employees.Add(e114);
            db.PhoneLines.Add(pl114);
            db.SaveChanges();
            e114 = db.Employees.FirstOrDefault(m => m.Name == "Octavio Rodriguez Valdes");
            pl114 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352166183");
            PhoneLineEmployee ple114 = new PhoneLineEmployee() { EmployeeId = e114.EmployeeId, PhoneNumber = pl114.PhoneNumber };
            SMSPlanAssignment spa114 = new SMSPlanAssignment() { PhoneNumber = pl114.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple114);
            db.SmsPlanAssignments.Add(spa114);
            db.SaveChanges();
            //
            Employee e115 = new Employee() { Name = "Omar Cobiella", Email = "omar.cobiella@mcvcomercial.cu", CostCenterCode = "20", PersonalCode = "724532" }; //59969798
            PhoneLine pl115 = new PhoneLine() { PhoneNumber = "5359969798", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "3385832", PIN = 1741, PUK = 72136414 };
            db.Employees.Add(e115);
            db.PhoneLines.Add(pl115);
            db.SaveChanges();
            e115 = db.Employees.FirstOrDefault(m => m.Name == "Omar Cobiella");
            pl115 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5359969798");
            PhoneLineEmployee ple115 = new PhoneLineEmployee() { EmployeeId = e115.EmployeeId, PhoneNumber = pl115.PhoneNumber };
            SMSPlanAssignment spa115 = new SMSPlanAssignment() { PhoneNumber = pl115.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple115);
            db.SmsPlanAssignments.Add(spa115);
            db.SaveChanges();
            //
            Employee e116 = new Employee() { Name = "Orestes Montalván", Email = "orestes.moltanval@mcvcomercial.cu", CostCenterCode = "21", Extension = "617" }; //52112632
            PhoneLine pl116 = new PhoneLine() { PhoneNumber = "5352112632", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "4926540", PIN = 9910, PUK = 5241723 };
            db.Employees.Add(e116);
            db.PhoneLines.Add(pl116);
            db.SaveChanges();
            e116 = db.Employees.FirstOrDefault(m => m.Name == "Orestes Montalván");
            pl116 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352112632");
            PhoneLineEmployee ple116 = new PhoneLineEmployee() { EmployeeId = e116.EmployeeId, PhoneNumber = pl116.PhoneNumber };
            SMSPlanAssignment spa116 = new SMSPlanAssignment() { PhoneNumber = pl116.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple116);
            db.SmsPlanAssignments.Add(spa116);
            db.SaveChanges();
            //
            Employee e117 = new Employee() { Name = "Orlando García", Email = "orlando.garcia@mcvcomercial.cu", CostCenterCode = "21", Extension = "605" }; //52680111
            PhoneLine pl117 = new PhoneLine() { PhoneNumber = "5352680111", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "224132", PIN = 2963, PUK = 13024284 };
            db.Employees.Add(e117);
            db.PhoneLines.Add(pl117);
            db.SaveChanges();
            e117 = db.Employees.FirstOrDefault(m => m.Name == "Orlando García");
            pl117 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352680111");
            PhoneLineEmployee ple117 = new PhoneLineEmployee() { EmployeeId = e117.EmployeeId, PhoneNumber = pl117.PhoneNumber };
            SMSPlanAssignment spa117 = new SMSPlanAssignment() { PhoneNumber = pl117.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple117);
            db.SmsPlanAssignments.Add(spa117);
            db.SaveChanges();
            //
            Employee e118 = new Employee() { Name = "Oscar Perez", CostCenterCode = "06" }; // 52142182
            PhoneLine pl118 = new PhoneLine() { PhoneNumber = "5352142182", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "6240174", PIN = 4776, PUK = 96532314 };
            db.Employees.Add(e118);
            db.PhoneLines.Add(pl118);
            db.SaveChanges();
            e118 = db.Employees.FirstOrDefault(m => m.Name == "Oscar Perez");
            pl118 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352142182");
            PhoneLineEmployee ple118 = new PhoneLineEmployee() { EmployeeId = e118.EmployeeId, PhoneNumber = pl118.PhoneNumber };
            SMSPlanAssignment spa118 = new SMSPlanAssignment() { PhoneNumber = pl118.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple118);
            db.SmsPlanAssignments.Add(spa118);
            db.SaveChanges();
            //
            Employee e119 = new Employee() { Name = "Oscar Suárez", Email = "oscar.suarez@mcvcomercial.cu", CostCenterCode = "29", Extension = "897", PersonalCode = "300617" }; //52166183
            PhoneLine pl119 = new PhoneLine() { PhoneNumber = "5352854656", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "272139", PIN = 3352, PUK = 61634461 };
            db.Employees.Add(e119);
            db.PhoneLines.Add(pl119);
            db.SaveChanges();
            e119 = db.Employees.FirstOrDefault(m => m.Name == "Oscar Suárez");
            pl119 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352854656");
            PhoneLineEmployee ple119 = new PhoneLineEmployee() { EmployeeId = e119.EmployeeId, PhoneNumber = pl119.PhoneNumber };
            SMSPlanAssignment spa119 = new SMSPlanAssignment() { PhoneNumber = pl119.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple119);
            db.SmsPlanAssignments.Add(spa119);
            db.SaveChanges();
            //
            Employee e120 = new Employee() { Name = "Osmel Mesa Issaqui", Email = "osmel.mesa@mcvcomercial.cu", CostCenterCode = "36", Extension = "816", PersonalCode = "291116" }; //52869102
            PhoneLine pl120 = new PhoneLine() { PhoneNumber = "5352869102", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "275908", PIN = 2191, PUK = 99463393 };
            db.Employees.Add(e120);
            db.PhoneLines.Add(pl120);
            db.SaveChanges();
            e120 = db.Employees.FirstOrDefault(m => m.Name == "Osmel Mesa Issaqui");
            pl120 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352869102");
            PhoneLineEmployee ple120 = new PhoneLineEmployee() { EmployeeId = e120.EmployeeId, PhoneNumber = pl120.PhoneNumber };
            SMSPlanAssignment spa120 = new SMSPlanAssignment() { PhoneNumber = pl120.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple120);
            db.SmsPlanAssignments.Add(spa120);
            db.SaveChanges();
            //
            Employee e121 = new Employee() { Name = "Osney Miranda Diago", Email = "osney.miranda@mcvcomercial.cu", CostCenterCode = "06", Extension = "715" }; //52125335
            PhoneLine pl121 = new PhoneLine() { PhoneNumber = "5352125335", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "5399954", PIN = 1110, PUK = 22482225 };
            db.Employees.Add(e121);
            db.PhoneLines.Add(pl121);
            db.SaveChanges();
            e121 = db.Employees.FirstOrDefault(m => m.Name == "Osney Miranda Diago");
            pl121 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352125335");
            PhoneLineEmployee ple121 = new PhoneLineEmployee() { EmployeeId = e121.EmployeeId, PhoneNumber = pl121.PhoneNumber };
            SMSPlanAssignment spa121 = new SMSPlanAssignment() { PhoneNumber = pl121.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple121);
            db.SmsPlanAssignments.Add(spa121);
            db.SaveChanges();
            //
            Employee e122 = new Employee() { Name = "Pablo González Gómez", Email = "pablo.gonzalez@mcvcomercial.cu", CostCenterCode = "19" }; //52166185
            PhoneLine pl122 = new PhoneLine() { PhoneNumber = "5352166185", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "3385832", PIN = 6786, PUK = 26467026 };
            db.Employees.Add(e122);
            db.PhoneLines.Add(pl122);
            db.SaveChanges();
            e122 = db.Employees.FirstOrDefault(m => m.Name == "Pablo González Gómez");
            pl122 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352166185");
            PhoneLineEmployee ple122 = new PhoneLineEmployee() { EmployeeId = e122.EmployeeId, PhoneNumber = pl122.PhoneNumber };
            SMSPlanAssignment spa122 = new SMSPlanAssignment() { PhoneNumber = pl122.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple122);
            db.SmsPlanAssignments.Add(spa122);
            db.SaveChanges();
            //
            Employee e123 = new Employee() { Name = "Pablo Viamontes", Email = "pablo.viamontes@mcvcomercial.cu", CostCenterCode = "32", Extension = "609" }; //52858670
            PhoneLine pl123 = new PhoneLine() { PhoneNumber = "5352858670", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "272141", PIN = 7553, PUK = 82234424 };
            db.Employees.Add(e123);
            db.PhoneLines.Add(pl123);
            db.SaveChanges();
            e123 = db.Employees.FirstOrDefault(m => m.Name == "Pablo Viamontes");
            pl123 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352858670");
            PhoneLineEmployee ple123 = new PhoneLineEmployee() { EmployeeId = e123.EmployeeId, PhoneNumber = pl123.PhoneNumber };
            SMSPlanAssignment spa123 = new SMSPlanAssignment() { PhoneNumber = pl123.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple123);
            db.SmsPlanAssignments.Add(spa123);
            db.SaveChanges();
            //
            Employee e124 = new Employee() { Name = "Pavel Nicolau", Email = "pablo.viamontes@mcvcomercial.cu", CostCenterCode = "12", Extension = "612" }; //52866931
            PhoneLine pl124 = new PhoneLine() { PhoneNumber = "5352866931", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "429548", PIN = 1915, PUK = 93995443 };
            db.Employees.Add(e124);
            db.PhoneLines.Add(pl124);
            db.SaveChanges();
            e124 = db.Employees.FirstOrDefault(m => m.Name == "Pavel Nicolau");
            pl124 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352866931");
            PhoneLineEmployee ple124 = new PhoneLineEmployee() { EmployeeId = e124.EmployeeId, PhoneNumber = pl124.PhoneNumber };
            SMSPlanAssignment spa124 = new SMSPlanAssignment() { PhoneNumber = pl124.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple124);
            db.SmsPlanAssignments.Add(spa124);
            db.SaveChanges();
            //
            Employee e125 = new Employee() { Name = "Pedro Mojena", Email = "pedro.mojena@mcvcomercial.cu", CostCenterCode = "11", Extension = "754" }; //52124878
            PhoneLine pl125 = new PhoneLine() { PhoneNumber = "5352124878", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "4926540", PIN = 2044, PUK = 14667423 };
            db.Employees.Add(e125);
            db.PhoneLines.Add(pl125);
            db.SaveChanges();
            e125 = db.Employees.FirstOrDefault(m => m.Name == "Pedro Mojena");
            pl125 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352124878");
            PhoneLineEmployee ple125 = new PhoneLineEmployee() { EmployeeId = e125.EmployeeId, PhoneNumber = pl125.PhoneNumber };
            SMSPlanAssignment spa125 = new SMSPlanAssignment() { PhoneNumber = pl125.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple125);
            db.SmsPlanAssignments.Add(spa125);
            db.SaveChanges();
            //
            Employee e126 = new Employee() { Name = "Pedro Rua", Email = "pedro.rua@mcvcomercial.cu", CostCenterCode = "19" }; //52805237
            PhoneLine pl126 = new PhoneLine() { PhoneNumber = "5352805237", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "260191", PIN = 4437, PUK = 51715562 };
            db.Employees.Add(e126);
            db.PhoneLines.Add(pl126);
            db.SaveChanges();
            e126 = db.Employees.FirstOrDefault(m => m.Name == "Pedro Rua");
            pl126 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352805237");
            PhoneLineEmployee ple126 = new PhoneLineEmployee() { EmployeeId = e126.EmployeeId, PhoneNumber = pl126.PhoneNumber };
            SMSPlanAssignment spa126 = new SMSPlanAssignment() { PhoneNumber = pl126.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple126);
            db.SmsPlanAssignments.Add(spa126);
            db.SaveChanges();
            //
            Employee e127 = new Employee() { Name = "Philip Priouzeau", Email = "philip.priouzeau@mcvcomercial.cu", CostCenterCode = "19" }; //52631723
            PhoneLine pl127 = new PhoneLine() { PhoneNumber = "5352631723", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "237742", PIN = 1525, PUK = 24208012 };
            db.Employees.Add(e127);
            db.PhoneLines.Add(pl127);
            db.SaveChanges();
            e127 = db.Employees.FirstOrDefault(m => m.Name == "Philip Priouzeau");
            pl127 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352631723");
            PhoneLineEmployee ple127 = new PhoneLineEmployee() { EmployeeId = e127.EmployeeId, PhoneNumber = pl127.PhoneNumber };
            SMSPlanAssignment spa127 = new SMSPlanAssignment() { PhoneNumber = pl127.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple127);
            db.SmsPlanAssignments.Add(spa127);
            db.SaveChanges();
            //
            Employee e128 = new Employee() { Name = "Rafael Hernández", Email = "rafael.hernandez@mcvcomercial.cu", CostCenterCode = "20", Extension = "887", PersonalCode = "243106" }; //52134596
            PhoneLine pl128 = new PhoneLine() { PhoneNumber = "5352134596", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "5480809", PIN = 2239, PUK = 6216671 };
            db.Employees.Add(e128);
            db.PhoneLines.Add(pl128);
            db.SaveChanges();
            e128 = db.Employees.FirstOrDefault(m => m.Name == "Rafael Hernández");
            pl128 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352134596");
            PhoneLineEmployee ple128 = new PhoneLineEmployee() { EmployeeId = e128.EmployeeId, PhoneNumber = pl128.PhoneNumber };
            SMSPlanAssignment spa128 = new SMSPlanAssignment() { PhoneNumber = pl128.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple128);
            db.SmsPlanAssignments.Add(spa128);
            db.SaveChanges();
            //
            Employee e129 = new Employee() { Name = "Raul Bacardi", Email = "raul.bacardi@mcvcomercial.cu", CostCenterCode = "01", Extension = "752", PersonalCode = "090919" }; //52121784
            PhoneLine pl129 = new PhoneLine() { PhoneNumber = "5352121784", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "5480809", PIN = 9082, PUK = 50041350 };
            db.Employees.Add(e129);
            db.PhoneLines.Add(pl129);
            db.SaveChanges();
            e129 = db.Employees.FirstOrDefault(m => m.Name == "Raul Bacardi");
            pl129 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352121784");
            PhoneLineEmployee ple129 = new PhoneLineEmployee() { EmployeeId = e129.EmployeeId, PhoneNumber = pl129.PhoneNumber };
            SMSPlanAssignment spa129 = new SMSPlanAssignment() { PhoneNumber = pl129.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple129);
            db.SmsPlanAssignments.Add(spa129);
            db.SaveChanges();
            //
            Employee e130 = new Employee() { Name = "Raul Gili", Email = "raul.gili@mcvcomercial.cu", CostCenterCode = "36", Extension = "812", PersonalCode = "100118" }; //52852688
            PhoneLine pl130 = new PhoneLine() { PhoneNumber = "5352852688", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "254560", PIN = 7581, PUK = 92103342 };
            db.Employees.Add(e130);
            db.PhoneLines.Add(pl130);
            db.SaveChanges();
            e130 = db.Employees.FirstOrDefault(m => m.Name == "Raul Gili");
            pl130 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352852688");
            PhoneLineEmployee ple130 = new PhoneLineEmployee() { EmployeeId = e130.EmployeeId, PhoneNumber = pl130.PhoneNumber };
            SMSPlanAssignment spa130 = new SMSPlanAssignment() { PhoneNumber = pl130.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple130);
            db.SmsPlanAssignments.Add(spa130);
            db.SaveChanges();
            //
            Employee e131 = new Employee() { Name = "Raul Ruiz Viera (7959119)", Email = "raul.ruiz@mcvcomercial.cu", CostCenterCode = "01", Extension = "795", PersonalCode = "450312" }; //52803803
            PhoneLine pl131 = new PhoneLine() { PhoneNumber = "5352803803", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "364877", PIN = 1455, PUK = 13523308 };
            db.Employees.Add(e131);
            db.PhoneLines.Add(pl131);
            db.SaveChanges();
            e131 = db.Employees.FirstOrDefault(m => m.Name == "Raul Ruiz Viera (7959119)");
            pl131 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352803803");
            PhoneLineEmployee ple131 = new PhoneLineEmployee() { EmployeeId = e131.EmployeeId, PhoneNumber = pl131.PhoneNumber };
            SMSPlanAssignment spa131 = new SMSPlanAssignment() { PhoneNumber = pl131.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple131);
            db.SmsPlanAssignments.Add(spa131);
            db.SaveChanges();
            //
            Employee e132 = new Employee() { Name = "Rene Ramos", CostCenterCode = "20" }; //52137132
            PhoneLine pl132 = new PhoneLine() { PhoneNumber = "5352137132", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "5983573", PIN = 7894, PUK = 65445438 };
            db.Employees.Add(e132);
            db.PhoneLines.Add(pl132);
            db.SaveChanges();
            e132 = db.Employees.FirstOrDefault(m => m.Name == "Rene Ramos");
            pl132 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352137132");
            PhoneLineEmployee ple132 = new PhoneLineEmployee() { EmployeeId = e132.EmployeeId, PhoneNumber = pl132.PhoneNumber };
            SMSPlanAssignment spa132 = new SMSPlanAssignment() { PhoneNumber = pl132.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple132);
            db.SmsPlanAssignments.Add(spa132);
            db.SaveChanges();
            //
            Employee e133 = new Employee() { Name = "Reserva 1", CostCenterCode = "02" }; //52166176
            PhoneLine pl133 = new PhoneLine() { PhoneNumber = "5352166176", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "498851", PIN = 8330, PUK = 79937211 };
            db.Employees.Add(e133);
            db.PhoneLines.Add(pl133);
            db.SaveChanges();
            e133 = db.Employees.FirstOrDefault(m => m.Name == "Reserva 1");
            pl133 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352166176");
            PhoneLineEmployee ple133 = new PhoneLineEmployee() { EmployeeId = e133.EmployeeId, PhoneNumber = pl133.PhoneNumber };
            SMSPlanAssignment spa133 = new SMSPlanAssignment() { PhoneNumber = pl133.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple133);
            db.SmsPlanAssignments.Add(spa133);
            db.SaveChanges();
            //
            Employee e134 = new Employee() { Name = "Reserva 2", CostCenterCode = "01" }; //52166180
            PhoneLine pl134 = new PhoneLine() { PhoneNumber = "5352166180", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "498851", PIN = 1607, PUK = 13097913 };
            db.Employees.Add(e134);
            db.PhoneLines.Add(pl134);
            db.SaveChanges();
            e134 = db.Employees.FirstOrDefault(m => m.Name == "Reserva 2");
            pl134 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352166180");
            PhoneLineEmployee ple134 = new PhoneLineEmployee() { EmployeeId = e134.EmployeeId, PhoneNumber = pl134.PhoneNumber };
            SMSPlanAssignment spa134 = new SMSPlanAssignment() { PhoneNumber = pl134.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple134);
            db.SmsPlanAssignments.Add(spa134);
            db.SaveChanges();
            //
            Employee e135 = new Employee() { Name = "Reserva 3", CostCenterCode = "01" }; //52809080
            PhoneLine pl135 = new PhoneLine() { PhoneNumber = "5352809080", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "268442", PIN = 4540, PUK = 6105604 };
            db.Employees.Add(e135);
            db.PhoneLines.Add(pl135);
            db.SaveChanges();
            e135 = db.Employees.FirstOrDefault(m => m.Name == "Reserva 3");
            pl135 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352809080");
            PhoneLineEmployee ple135 = new PhoneLineEmployee() { EmployeeId = e135.EmployeeId, PhoneNumber = pl135.PhoneNumber };
            SMSPlanAssignment spa135 = new SMSPlanAssignment() { PhoneNumber = pl135.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple135);
            db.SmsPlanAssignments.Add(spa135);
            db.SaveChanges();
            //
            Employee e136 = new Employee() { Name = "Reynier Paneque", Email = "reynier.paneque@mcvcomercial.cu", CostCenterCode = "29", Extension = "827", PersonalCode = "596111" }; // 52876633
            PhoneLine pl136 = new PhoneLine() { PhoneNumber = "5352876633", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "670868", PIN = 5921, PUK = 40620956 };
            db.Employees.Add(e136);
            db.PhoneLines.Add(pl136);
            db.SaveChanges();
            e136 = db.Employees.FirstOrDefault(m => m.Name == "Reynier Paneque");
            pl136 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352876633");
            PhoneLineEmployee ple136 = new PhoneLineEmployee() { EmployeeId = e136.EmployeeId, PhoneNumber = pl136.PhoneNumber };
            SMSPlanAssignment spa136 = new SMSPlanAssignment() { PhoneNumber = pl136.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = sp25SoloRec.SMSPlanId };
            db.PhoneLineEmployees.Add(ple136);
            db.SmsPlanAssignments.Add(spa136);
            db.SaveChanges();
            //
            Employee e137 = new Employee() { Name = "Ricardo Garcia Gatell", Email = "ricardo.garcia@mcvcomercial.cu", CostCenterCode = "02", Extension = "721" }; //52863870
            PhoneLine pl137 = new PhoneLine() { PhoneNumber = "5352863870", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "275908", PIN = 9385, PUK = 70126118 };
            db.Employees.Add(e137);
            db.PhoneLines.Add(pl137);
            db.SaveChanges();
            e137 = db.Employees.FirstOrDefault(m => m.Name == "Ricardo Garcia Gatell");
            pl137 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352863870");
            PhoneLineEmployee ple137 = new PhoneLineEmployee() { EmployeeId = e137.EmployeeId, PhoneNumber = pl137.PhoneNumber };
            SMSPlanAssignment spa137 = new SMSPlanAssignment() { PhoneNumber = pl137.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spVIP.SMSPlanId };
            db.PhoneLineEmployees.Add(ple137);
            db.SmsPlanAssignments.Add(spa137);
            db.SaveChanges();
            //
            Employee e138 = new Employee() { Name = "Ricardo Quintana", Email = "ricardo.quintana@mcvcomercial.cu", CostCenterCode = "01", PersonalCode = "080720" }; //52134595
            PhoneLine pl138 = new PhoneLine() { PhoneNumber = "5352134595", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "5480809", PIN = 1331, PUK = 42481622 };
            db.Employees.Add(e138);
            db.PhoneLines.Add(pl138);
            db.SaveChanges();
            e138 = db.Employees.FirstOrDefault(m => m.Name == "Ricardo Quintana");
            pl138 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352134595");
            PhoneLineEmployee ple138 = new PhoneLineEmployee() { EmployeeId = e138.EmployeeId, PhoneNumber = pl138.PhoneNumber };
            SMSPlanAssignment spa138 = new SMSPlanAssignment() { PhoneNumber = pl138.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple138);
            db.SmsPlanAssignments.Add(spa138);
            db.SaveChanges();
            //
            Employee e139 = new Employee() { Name = "Ricardo Rodriguez Batista", Email = "ricardo.rodriguez@mcvcomercial.cu", CostCenterCode = "19" }; //52166175
            PhoneLine pl139 = new PhoneLine() { PhoneNumber = "5352166175", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "3385832", PIN = 302, PUK = 23934089 };
            db.Employees.Add(e139);
            db.PhoneLines.Add(pl139);
            db.SaveChanges();
            e139 = db.Employees.FirstOrDefault(m => m.Name == "Ricardo Rodriguez Batista");
            pl139 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352166175");
            PhoneLineEmployee ple139 = new PhoneLineEmployee() { EmployeeId = e139.EmployeeId, PhoneNumber = pl139.PhoneNumber };
            SMSPlanAssignment spa139 = new SMSPlanAssignment() { PhoneNumber = pl139.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple139);
            db.SmsPlanAssignments.Add(spa139);
            db.SaveChanges();
            //
            Employee e140 = new Employee() { Name = "Rigoberto Landeiro", CostCenterCode = "07" }; //52871782
            PhoneLine pl140 = new PhoneLine() { PhoneNumber = "5352871782", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "564211", PIN = 3376, PUK = 84265597 };
            db.Employees.Add(e140);
            db.PhoneLines.Add(pl140);
            db.SaveChanges();
            e140 = db.Employees.FirstOrDefault(m => m.Name == "Rigoberto Landeiro");
            pl140 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352871782");
            PhoneLineEmployee ple140 = new PhoneLineEmployee() { EmployeeId = e140.EmployeeId, PhoneNumber = pl140.PhoneNumber };
            SMSPlanAssignment spa140 = new SMSPlanAssignment() { PhoneNumber = pl140.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = sp25SoloRec.SMSPlanId };
            db.PhoneLineEmployees.Add(ple140);
            db.SmsPlanAssignments.Add(spa140);
            db.SaveChanges();
            //
            Employee e141 = new Employee() { Name = "Rino Osorio", Email = "rino.osorio@mcvcomercial.cu", CostCenterCode = "20" }; //59956587
            PhoneLine pl141 = new PhoneLine() { PhoneNumber = "5359956587", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "", PIN = 0, PUK = 0 };
            db.Employees.Add(e141);
            db.PhoneLines.Add(pl141);
            db.SaveChanges();
            e141 = db.Employees.FirstOrDefault(m => m.Name == "Rino Osorio");
            pl141 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5359956587");
            //CallingPlanAssignment cpa140 = new CallingPlanAssignment() { CallingPlanId = cp25SoloRec.CallingPlanId, PhoneNumber = pl141.PhoneNumber, Month = 1, Year = 2020 };
            PhoneLineEmployee ple141 = new PhoneLineEmployee() { EmployeeId = e141.EmployeeId, PhoneNumber = pl141.PhoneNumber };
            SMSPlanAssignment spa141 = new SMSPlanAssignment() { PhoneNumber = pl141.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple141);
            db.SmsPlanAssignments.Add(spa141);
            db.SaveChanges();
            //
            Employee e142 = new Employee() { Name = "Rodney Alfaro", CostCenterCode = "30" }; // 52151913
            PhoneLine pl142 = new PhoneLine() { PhoneNumber = "5352151913", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "6425317", PIN = 8149, PUK = 38898460 };
            db.Employees.Add(e142);
            db.PhoneLines.Add(pl142);
            db.SaveChanges();
            e142 = db.Employees.FirstOrDefault(m => m.Name == "Rodney Alfaro");
            pl142 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352151913");
            PhoneLineEmployee ple142 = new PhoneLineEmployee() { EmployeeId = e142.EmployeeId, PhoneNumber = pl142.PhoneNumber };
            SMSPlanAssignment spa142 = new SMSPlanAssignment() { PhoneNumber = pl142.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple142);
            db.SmsPlanAssignments.Add(spa142);
            db.SaveChanges();
            //
            Employee e143 = new Employee() { Name = "Rodolfo Mc.intoch (Almacen)", Email = "rodolfo.mcintoch@mcvcomercial.cu", CostCenterCode = "20", Extension = "842" }; //52872585
            PhoneLine pl143 = new PhoneLine() { PhoneNumber = "5352872585", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "498584", PIN = 8987, PUK = 21525497 };
            db.Employees.Add(e143);
            db.PhoneLines.Add(pl143);
            db.SaveChanges();
            e143 = db.Employees.FirstOrDefault(m => m.Name == "Rodolfo Mc.intoch (Almacen)");
            pl143 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352872585");
            PhoneLineEmployee ple143 = new PhoneLineEmployee() { EmployeeId = e143.EmployeeId, PhoneNumber = pl143.PhoneNumber };
            SMSPlanAssignment spa143 = new SMSPlanAssignment() { PhoneNumber = pl143.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = sp25SoloRec.SMSPlanId };
            db.PhoneLineEmployees.Add(ple143);
            db.SmsPlanAssignments.Add(spa143);
            db.SaveChanges();
            //
            Employee e144 = new Employee() { Name = "Rossie Zambrana", Email = "rossie.zambrana@mcvcomercial.cu", CostCenterCode = "29", Extension = "802", PersonalCode = "101103" }; //52640680
            PhoneLine pl144 = new PhoneLine() { PhoneNumber = "5352640680", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "255768", PIN = 596, PUK = 63060670 };
            db.Employees.Add(e144);
            db.PhoneLines.Add(pl144);
            db.SaveChanges();
            e144 = db.Employees.FirstOrDefault(m => m.Name == "Rossie Zambrana");
            pl144 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352640680");
            PhoneLineEmployee ple144 = new PhoneLineEmployee() { EmployeeId = e144.EmployeeId, PhoneNumber = pl144.PhoneNumber };
            SMSPlanAssignment spa144 = new SMSPlanAssignment() { PhoneNumber = pl144.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple144);
            db.SmsPlanAssignments.Add(spa144);
            db.SaveChanges();
            //
            Employee e145 = new Employee() { Name = "Santiago Duarte", CostCenterCode = "19" }; // 59969796
            PhoneLine pl145 = new PhoneLine() { PhoneNumber = "5359969796", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "564211", PIN = 7125, PUK = 4422917 };
            db.Employees.Add(e145);
            db.PhoneLines.Add(pl145);
            db.SaveChanges();
            e145 = db.Employees.FirstOrDefault(m => m.Name == "Santiago Duarte");
            pl145 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5359969796");
            PhoneLineEmployee ple145 = new PhoneLineEmployee() { EmployeeId = e145.EmployeeId, PhoneNumber = pl145.PhoneNumber };
            SMSPlanAssignment spa145 = new SMSPlanAssignment() { PhoneNumber = pl145.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = sp25SoloRec.SMSPlanId };
            db.PhoneLineEmployees.Add(ple145);
            db.SmsPlanAssignments.Add(spa145);
            db.SaveChanges();
            //
            Employee e146 = new Employee() { Name = "Susel Cañete", Email = "susel.canete@mcvcomercial.cu", CostCenterCode = "16", Extension = "763", PersonalCode = "281016" }; //52122174
            PhoneLine pl146 = new PhoneLine() { PhoneNumber = "5352122174", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "5625342", PIN = 5224, PUK = 19592313 };
            db.Employees.Add(e146);
            db.PhoneLines.Add(pl146);
            db.SaveChanges();
            e146 = db.Employees.FirstOrDefault(m => m.Name == "Susel Cañete");
            pl146 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352122174");
            PhoneLineEmployee ple146 = new PhoneLineEmployee() { EmployeeId = e146.EmployeeId, PhoneNumber = pl146.PhoneNumber };
            SMSPlanAssignment spa146 = new SMSPlanAssignment() { PhoneNumber = pl146.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple146);
            db.SmsPlanAssignments.Add(spa146);
            db.SaveChanges();
            //
            Employee e147 = new Employee() { Name = "Tania Cisneros", Email = "tania.cisneros@mcvcomercial.cu", CostCenterCode = "01", Extension = "776" }; //52152053
            PhoneLine pl147 = new PhoneLine() { PhoneNumber = "5352152053", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "3385832", PIN = 3538, PUK = 31175403 };
            db.Employees.Add(e147);
            db.PhoneLines.Add(pl147);
            db.SaveChanges();
            e147 = db.Employees.FirstOrDefault(m => m.Name == "Tania Cisneros");
            pl147 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352152053");
            PhoneLineEmployee ple147 = new PhoneLineEmployee() { EmployeeId = e147.EmployeeId, PhoneNumber = pl147.PhoneNumber };
            SMSPlanAssignment spa147 = new SMSPlanAssignment() { PhoneNumber = pl147.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple147);
            db.SmsPlanAssignments.Add(spa147);
            db.SaveChanges();
            //
            Employee e148 = new Employee() { Name = "Tania Segura", Email = "tania.segura@mcvcomercial.cu", CostCenterCode = "05", Extension = "797", PersonalCode = "033113" }; //52630274
            PhoneLine pl148 = new PhoneLine() { PhoneNumber = "5352630274", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "264005", PIN = 2222, PUK = 51111217 };
            db.Employees.Add(e148);
            db.PhoneLines.Add(pl148);
            db.SaveChanges();
            e148 = db.Employees.FirstOrDefault(m => m.Name == "Tania Segura");
            pl148 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352630274");
            PhoneLineEmployee ple148 = new PhoneLineEmployee() { EmployeeId = e148.EmployeeId, PhoneNumber = pl148.PhoneNumber };
            SMSPlanAssignment spa148 = new SMSPlanAssignment() { PhoneNumber = pl148.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple148);
            db.SmsPlanAssignments.Add(spa148);
            db.SaveChanges();
            //
            Employee e149 = new Employee() { Name = "Vladimir Alfonso", Email = "taller.industriales@mcvcomercial.cu", CostCenterCode = "33", Extension = "837", PersonalCode = "190218" }; //52121783
            PhoneLine pl149 = new PhoneLine() { PhoneNumber = "5352121783", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "5480809", PIN = 1518, PUK = 66221903 };
            db.Employees.Add(e149);
            db.PhoneLines.Add(pl149);
            db.SaveChanges();
            e149 = db.Employees.FirstOrDefault(m => m.Name == "Vladimir Alfonso");
            pl149 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352121783");
            PhoneLineEmployee ple149 = new PhoneLineEmployee() { EmployeeId = e149.EmployeeId, PhoneNumber = pl149.PhoneNumber };
            SMSPlanAssignment spa149 = new SMSPlanAssignment() { PhoneNumber = pl149.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple149);
            db.SmsPlanAssignments.Add(spa149);
            db.SaveChanges();
            //
            Employee e150 = new Employee() { Name = "Vladimir Castané", Email = "vladimir.castane@mcvcomercial.cu", CostCenterCode = "36", PersonalCode = "251018" }; //52803856
            PhoneLine pl150 = new PhoneLine() { PhoneNumber = "5352803856", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "916190", PIN = 8177, PUK = 75680163 };
            db.Employees.Add(e150);
            db.PhoneLines.Add(pl150);
            db.SaveChanges();
            e150 = db.Employees.FirstOrDefault(m => m.Name == "Vladimir Castané");
            pl150 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352803856");
            PhoneLineEmployee ple150 = new PhoneLineEmployee() { EmployeeId = e150.EmployeeId, PhoneNumber = pl150.PhoneNumber };
            SMSPlanAssignment spa150 = new SMSPlanAssignment() { PhoneNumber = pl150.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple150);
            db.SmsPlanAssignments.Add(spa150);
            db.SaveChanges();
            //
            Employee e151 = new Employee() { Name = "Vladimir Castro", Email = "vladimir.castro@mcvcomercial.cu", CostCenterCode = "19" }; //52865670
            PhoneLine pl151 = new PhoneLine() { PhoneNumber = "5352865670", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "293867", PIN = 1880, PUK = 74050753 };
            db.Employees.Add(e151);
            db.PhoneLines.Add(pl151);
            db.SaveChanges();
            e151 = db.Employees.FirstOrDefault(m => m.Name == "Vladimir Castro");
            pl151 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352865670");
            PhoneLineEmployee ple151 = new PhoneLineEmployee() { EmployeeId = e151.EmployeeId, PhoneNumber = pl151.PhoneNumber };
            SMSPlanAssignment spa151 = new SMSPlanAssignment() { PhoneNumber = pl151.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple151);
            db.SmsPlanAssignments.Add(spa151);
            db.SaveChanges();
            //
            Employee e152 = new Employee() { Name = "Yadian González", CostCenterCode = "19" }; // 52872583
            PhoneLine pl152 = new PhoneLine() { PhoneNumber = "5352872583", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "498582", PIN = 3765, PUK = 15456130 };
            db.Employees.Add(e152);
            db.PhoneLines.Add(pl152);
            db.SaveChanges();
            e152 = db.Employees.FirstOrDefault(m => m.Name == "Yadian González");
            pl152 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352872583");
            PhoneLineEmployee ple152 = new PhoneLineEmployee() { EmployeeId = e152.EmployeeId, PhoneNumber = pl152.PhoneNumber };
            SMSPlanAssignment spa152 = new SMSPlanAssignment() { PhoneNumber = pl152.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = sp25SoloRec.SMSPlanId };
            db.PhoneLineEmployees.Add(ple152);
            db.SmsPlanAssignments.Add(spa152);
            db.SaveChanges();
            //
            Employee e153 = new Employee() { Name = "Yaimara Pimentel", Email = "yaimara.pimentel@mcvcomercial.cu", CostCenterCode = "20", Extension = "882", PersonalCode = "150816" }; //52871928
            PhoneLine pl153 = new PhoneLine() { PhoneNumber = "5352871928", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "819031", PIN = 3196, PUK = 48648566 };
            db.Employees.Add(e153);
            db.PhoneLines.Add(pl153);
            db.SaveChanges();
            e153 = db.Employees.FirstOrDefault(m => m.Name == "Yaimara Pimentel");
            pl153 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352871928");
            PhoneLineEmployee ple153 = new PhoneLineEmployee() { EmployeeId = e153.EmployeeId, PhoneNumber = pl153.PhoneNumber };
            SMSPlanAssignment spa153 = new SMSPlanAssignment() { PhoneNumber = pl153.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = sp25SoloRec.SMSPlanId };
            db.PhoneLineEmployees.Add(ple153);
            db.SmsPlanAssignments.Add(spa153);
            db.SaveChanges();
            //
            Employee e154 = new Employee() { Name = "Yairon Tejeda", CostCenterCode = "34" }; //52151918
            PhoneLine pl154 = new PhoneLine() { PhoneNumber = "5352151918", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "6425317", PIN = 7466, PUK = 50334561 };
            db.Employees.Add(e154);
            db.PhoneLines.Add(pl154);
            db.SaveChanges();
            e154 = db.Employees.FirstOrDefault(m => m.Name == "Yairon Tejeda");
            pl154 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352151918");
            PhoneLineEmployee ple154 = new PhoneLineEmployee() { EmployeeId = e154.EmployeeId, PhoneNumber = pl154.PhoneNumber };
            SMSPlanAssignment spa154 = new SMSPlanAssignment() { PhoneNumber = pl154.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple154);
            db.SmsPlanAssignments.Add(spa154);
            db.SaveChanges();
            //
            Employee e155 = new Employee() { Name = "Yalile Fernandez Betancourt", Email = "yalile.fernandez@mcvcomercial.cu", CostCenterCode = "01" }; //52121788
            PhoneLine pl155 = new PhoneLine() { PhoneNumber = "5352121788", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "5480809", PIN = 1607, PUK = 13097913 };
            db.Employees.Add(e155);
            db.PhoneLines.Add(pl155);
            db.SaveChanges();
            e155 = db.Employees.FirstOrDefault(m => m.Name == "Yalile Fernandez Betancourt");
            pl155 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352121788");
            PhoneLineEmployee ple155 = new PhoneLineEmployee() { EmployeeId = e155.EmployeeId, PhoneNumber = pl155.PhoneNumber };
            SMSPlanAssignment spa155 = new SMSPlanAssignment() { PhoneNumber = pl155.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple155);
            db.SmsPlanAssignments.Add(spa155);
            db.SaveChanges();
            //
            Employee e156 = new Employee() { Name = "Yanelcy Cabrera Ferrer", Email = "yanelcy.cabrera@mcvcomercial.cu", CostCenterCode = "20", Extension = "881", PersonalCode = "091116" }; //52125178
            PhoneLine pl156 = new PhoneLine() { PhoneNumber = "5352125178", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "5401565", PIN = 3044, PUK = 12132010 };
            db.Employees.Add(e156);
            db.PhoneLines.Add(pl156);
            db.SaveChanges();
            e156 = db.Employees.FirstOrDefault(m => m.Name == "Yanelcy Cabrera Ferrer");
            pl156 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352125178");
            PhoneLineEmployee ple156 = new PhoneLineEmployee() { EmployeeId = e156.EmployeeId, PhoneNumber = pl156.PhoneNumber };
            SMSPlanAssignment spa156 = new SMSPlanAssignment() { PhoneNumber = pl156.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple156);
            db.SmsPlanAssignments.Add(spa156);
            db.SaveChanges();
            //
            Employee e157 = new Employee() { Name = "Yanin Llanes Hernandez", Email = "yanin.llanes@mcvcomercial.cu", CostCenterCode = "01", Extension = "814", PersonalCode = "070316" }; //52160875
            PhoneLine pl157 = new PhoneLine() { PhoneNumber = "5352160875", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "3385832", PIN = 3238, PUK = 73762176 };
            db.Employees.Add(e157);
            db.PhoneLines.Add(pl157);
            db.SaveChanges();
            e157 = db.Employees.FirstOrDefault(m => m.Name == "Yanin Llanes Hernandez");
            pl157 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352160875");
            PhoneLineEmployee ple157 = new PhoneLineEmployee() { EmployeeId = e157.EmployeeId, PhoneNumber = pl157.PhoneNumber };
            SMSPlanAssignment spa157 = new SMSPlanAssignment() { PhoneNumber = pl157.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = sp25SoloRec.SMSPlanId };
            db.PhoneLineEmployees.Add(ple157);
            db.SmsPlanAssignments.Add(spa157);
            db.SaveChanges();
            //
            Employee e158 = new Employee() { Name = "Yanisey Izquierdo", Email = "yanisey.izquierdo@mcvcomercial.cu", CostCenterCode = "16", Extension = "755", PersonalCode = "181151" }; //52630150
            PhoneLine pl158 = new PhoneLine() { PhoneNumber = "5352630150", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "1499507", PIN = 9060, PUK = 49986367 };
            db.Employees.Add(e158);
            db.PhoneLines.Add(pl158);
            db.SaveChanges();
            e158 = db.Employees.FirstOrDefault(m => m.Name == "Yanisey Izquierdo");
            pl158 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352630150");
            PhoneLineEmployee ple158 = new PhoneLineEmployee() { EmployeeId = e158.EmployeeId, PhoneNumber = pl158.PhoneNumber };
            SMSPlanAssignment spa158 = new SMSPlanAssignment() { PhoneNumber = pl158.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple158);
            db.SmsPlanAssignments.Add(spa158);
            db.SaveChanges();
            //
            Employee e159 = new Employee() { Name = "Yoeslan Valdes sanchez", CostCenterCode = "31" }; //52134602
            PhoneLine pl159 = new PhoneLine() { PhoneNumber = "5352134602", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "5480809", PIN = 2926, PUK = 2907790 };
            db.Employees.Add(e159);
            db.PhoneLines.Add(pl159);
            db.SaveChanges();
            e159 = db.Employees.FirstOrDefault(m => m.Name == "Yoeslan Valdes Sanchez");
            pl159 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352134602");
            PhoneLineEmployee ple159 = new PhoneLineEmployee() { EmployeeId = e159.EmployeeId, PhoneNumber = pl159.PhoneNumber };
            SMSPlanAssignment spa159 = new SMSPlanAssignment() { PhoneNumber = pl159.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple159);
            db.SmsPlanAssignments.Add(spa159);
            db.SaveChanges();
            //
            Employee e160 = new Employee() { Name = "Yordy Mejias", Email = "yordy.mejias@mcvcomercial.cu", CostCenterCode = "37", }; //52093821
            PhoneLine pl160 = new PhoneLine() { PhoneNumber = "5352093821", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "4245905", PIN = 9122, PUK = 43879290 };
            db.Employees.Add(e160);
            db.PhoneLines.Add(pl160);
            db.SaveChanges();
            e160 = db.Employees.FirstOrDefault(m => m.Name == "Yordy Mejias");
            pl160 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352093821");
            PhoneLineEmployee ple160 = new PhoneLineEmployee() { EmployeeId = e160.EmployeeId, PhoneNumber = pl160.PhoneNumber };
            SMSPlanAssignment spa160 = new SMSPlanAssignment() { PhoneNumber = pl160.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple160);
            db.SmsPlanAssignments.Add(spa160);
            db.SaveChanges();
            //
            Employee e161 = new Employee() { Name = "Yorelis Martinez Lara", Email = "yorelis.martinez@mcvcomercial.cu", CostCenterCode = "20", Extension = "892", PersonalCode = "141019" }; //59969801
            PhoneLine pl161 = new PhoneLine() { PhoneNumber = "5359969801", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "3385832", PIN = 1022, PUK = 11222211 };
            db.Employees.Add(e161);
            db.PhoneLines.Add(pl161);
            db.SaveChanges();
            e161 = db.Employees.FirstOrDefault(m => m.Name == "Yorelis Martinez Lara");
            pl161 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5359969801");
            PhoneLineEmployee ple161 = new PhoneLineEmployee() { EmployeeId = e161.EmployeeId, PhoneNumber = pl161.PhoneNumber };
            SMSPlanAssignment spa161 = new SMSPlanAssignment() { PhoneNumber = pl161.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple161);
            db.SmsPlanAssignments.Add(spa161);
            db.SaveChanges();
            //
            Employee e162 = new Employee() { Name = "Yorky Rodriguez", CostCenterCode = "19" }; //52154205
            PhoneLine pl162 = new PhoneLine() { PhoneNumber = "5352154205", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "3385832", PIN = 0, PUK = 66833061 };
            db.Employees.Add(e162);
            db.PhoneLines.Add(pl162);
            db.SaveChanges();
            e162 = db.Employees.FirstOrDefault(m => m.Name == "Yorky Rodriguez");
            pl162 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352154205");
            PhoneLineEmployee ple162 = new PhoneLineEmployee() { EmployeeId = e162.EmployeeId, PhoneNumber = pl162.PhoneNumber };
            SMSPlanAssignment spa162 = new SMSPlanAssignment() { PhoneNumber = pl162.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple162);
            db.SmsPlanAssignments.Add(spa162);
            db.SaveChanges();
            //
            Employee e163 = new Employee() { Name = "Yosvanis Reyes Lores", Email = "yosvanis.reyes@mcvcomercial.cu", CostCenterCode = "36", }; //52093822
            PhoneLine pl163 = new PhoneLine() { PhoneNumber = "5352093822", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "4245915", PIN = 5299, PUK = 29298214 };
            db.Employees.Add(e163);
            db.PhoneLines.Add(pl163);
            db.SaveChanges();
            e163 = db.Employees.FirstOrDefault(m => m.Name == "Yosvanis Reyes Lores");
            pl163 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352093822");
            PhoneLineEmployee ple163 = new PhoneLineEmployee() { EmployeeId = e163.EmployeeId, PhoneNumber = pl163.PhoneNumber };
            SMSPlanAssignment spa163 = new SMSPlanAssignment() { PhoneNumber = pl163.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple163);
            db.SmsPlanAssignments.Add(spa163);
            db.SaveChanges();
            //
            Employee e164 = new Employee() { Name = "Yovani Regalado", CostCenterCode = "20", }; //52137131
            PhoneLine pl164 = new PhoneLine() { PhoneNumber = "5352137131", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "5983573", PIN = 7894, PUK = 65445438 };
            db.Employees.Add(e164);
            db.PhoneLines.Add(pl164);
            db.SaveChanges();
            e164 = db.Employees.FirstOrDefault(m => m.Name == "Yovani Regalado");
            pl164 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352137131");
            PhoneLineEmployee ple164 = new PhoneLineEmployee() { EmployeeId = e164.EmployeeId, PhoneNumber = pl164.PhoneNumber };
            SMSPlanAssignment spa164 = new SMSPlanAssignment() { PhoneNumber = pl164.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple164);
            db.SmsPlanAssignments.Add(spa164);
            db.SaveChanges();
            //
            Employee e165 = new Employee() { Name = "Yuri Ramirez Leyva", Email = "yuri.ramirez@mcvcomercial.cu", CostCenterCode = "29", }; //52166082
            PhoneLine pl165 = new PhoneLine() { PhoneNumber = "5352166082", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "3385832", PIN = 5359, PUK = 30175004 };
            db.Employees.Add(e165);
            db.PhoneLines.Add(pl165);
            db.SaveChanges();
            e165 = db.Employees.FirstOrDefault(m => m.Name == "Yuri Ramirez Leyva");
            pl165 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352166082");
            PhoneLineEmployee ple165 = new PhoneLineEmployee() { EmployeeId = e165.EmployeeId, PhoneNumber = pl165.PhoneNumber };
            SMSPlanAssignment spa165 = new SMSPlanAssignment() { PhoneNumber = pl165.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple165);
            db.SmsPlanAssignments.Add(spa165);
            db.SaveChanges();
            //
            Employee e166 = new Employee() { Name = "Yusself Núñez Ramirez", Email = "yusself.nunez@mcvcomercial.cu", CostCenterCode = "29", Extension = "627" }; // 52166181
            PhoneLine pl166 = new PhoneLine() { PhoneNumber = "5352166181", CallsDetails = false, SMSDetails = false, GPRSDetails = false, Contract = "3385832", PIN = 7499, PUK = 90079829 };
            db.Employees.Add(e166);
            db.PhoneLines.Add(pl166);
            db.SaveChanges();
            e166 = db.Employees.FirstOrDefault(m => m.Name == "Yusself Núñez Ramirez");
            pl166 = db.PhoneLines.FirstOrDefault(m => m.PhoneNumber == "5352166181");
            PhoneLineEmployee ple166 = new PhoneLineEmployee() { EmployeeId = e166.EmployeeId, PhoneNumber = pl166.PhoneNumber };
            SMSPlanAssignment spa166 = new SMSPlanAssignment() { PhoneNumber = pl166.PhoneNumber, Year = 2020, Month = 1, SMSPlanId = spLow.SMSPlanId };
            db.PhoneLineEmployees.Add(ple166);
            db.SmsPlanAssignments.Add(spa166);
            db.SaveChanges();
            //





        }
    }
}