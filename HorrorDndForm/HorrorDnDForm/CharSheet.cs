using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HorrorDnDForm
{
    public partial class CharSheet : Form
    {
        String charName;
        int lvl;
        int probo;
        int str, dex, con, intel, wis, cha;
        int sBonus, dBonus, coBonus, iBonus, wBonus, chBonus;
        bool isNum;
        int armorEquipped = 10;
        bool AcrX, AniX, ArcX, AthX, DecX, HisX, InsX, IntX, InvX, MedX, NatX, PercX, PerfX, PersX, RelX, SleX, SteX, SurX;
        bool STRX, DEXX, CONX, INTELX, WISX, CHAX;
        int healDmg;
        int currHealth, maxHealth;
        int insanity;
        bool deathSave1, deathSave2, deathSave3;
        int d4N, d6N, d8N, d12N, d20N;
        int diceTotal;
        Random rand = new Random();

        public CharSheet()
        {
            InitializeComponent();
        }
        private void CharSheet_Load(object sender, EventArgs e)
        {
            try
            {
                TextReader tr = new StreamReader("SavedSheet.txt");

                charName = tr.ReadLine();
                lvl = Convert.ToInt32(tr.ReadLine());
                insanity = Convert.ToInt32(tr.ReadLine());
                currHealth = Convert.ToInt32(tr.ReadLine());
                maxHealth = Convert.ToInt32(tr.ReadLine());
                str = Convert.ToInt32(tr.ReadLine());
                dex = Convert.ToInt32(tr.ReadLine());
                con = Convert.ToInt32(tr.ReadLine());
                intel = Convert.ToInt32(tr.ReadLine());
                wis = Convert.ToInt32(tr.ReadLine());
                cha = Convert.ToInt32(tr.ReadLine());

                tr.Close();
            }
            catch(FileNotFoundException fnfe)
            {
                lvl = 1;
                probo = 2;
                currHealth = -1;
                maxHealth = 0;
                insanity = 0;
                deathSave1 = false;
                deathSave2 = false;
                deathSave3 = false;
            }
            try
            {
                TextReader tr1 = new StreamReader("SheetNotes.txt");
                txtNotes.Text = tr1.ReadToEnd();
                tr1.Close();
            } 
            catch(FileNotFoundException fnfe)
            {
                txtNotes.Text = "";
            }
                txtName.Text = charName;
            LVL.Text = lvl.ToString();
            Insanity.Text = insanity.ToString();
            updateHealth();
            STR.Text = str.ToString();
            DEX.Text = dex.ToString();
            CON.Text = con.ToString();
            INT.Text = intel.ToString();
            WIS.Text = wis.ToString();
            CHA.Text = cha.ToString();
        }
        private void txtName_TextChanged(object sender, EventArgs e)
        {
            charName = txtName.Text;
        }
        private void STR_TextChanged(object sender, EventArgs e)
        {
            isNum = int.TryParse(STR.Text, out str);
            if (!isNum && STR.Text != "")
            {
                STR.Text = "";
                str = 0;
            }
            else
            {
                sBonus = (str - 10)/2;
                updateStr(sBonus);
            }
        }
        private void DEX_TextChanged(object sender, EventArgs e)
        {
            isNum = int.TryParse(DEX.Text, out dex);
            if (!isNum && DEX.Text != "")
            {
                DEX.Text = "";
                dex = 0;
            }
            else
            {
                dBonus = (dex - 10) / 2;
                updateDex(dBonus);
            }
        }
        private void CON_TextChanged(object sender, EventArgs e)
        {
            isNum = int.TryParse(CON.Text, out con);
            if (!isNum && CON.Text != "")
            {
                CON.Text = "";
                con = 0;
            }
            else
            {
                coBonus = (con - 10) / 2;
                updateCon(coBonus);
            }
        }
        private void INT_TextChanged(object sender, EventArgs e)
        {
            isNum = int.TryParse(INT.Text, out intel);
            if (!isNum && INT.Text != "")
            {
                INT.Text = "";
                intel = 0;
            }
            else
            {
                iBonus = (intel - 10) / 2;
                updateInt(iBonus);
            }
        }
        private void WIS_TextChanged(object sender, EventArgs e)
        {
            isNum = int.TryParse(WIS.Text, out wis);
            if (!isNum && WIS.Text != "")
            {
                WIS.Text = "";
                wis = 0;
            }
            else
            {
                wBonus = (wis - 10) / 2;
                updateWis(sBonus);
            }
        }
        private void CHA_TextChanged(object sender, EventArgs e)
        {
            isNum = int.TryParse(CHA.Text, out cha);
            if (!isNum && CHA.Text != "")
            {
                CHA.Text = "";
                cha = 0;
            }
            else
            {
                chBonus = (cha - 10) / 2;
                updateCha(chBonus);
            }
        }
        private void updateStr(int sBonus)
        {
            skillAthN.Text = sBonus.ToString();
            stSTR.Text = sBonus.ToString();
            sBo.Text = sBonus.ToString();
        }        private void updateDex(int dBonus)
        {
            skillAcrN.Text = dBonus.ToString();
            skillSleN.Text = dBonus.ToString();
            skillSteN.Text = dBonus.ToString();
            AC.Text = (armorEquipped + dBonus).ToString();
            Initiative.Text = "+" + dBonus.ToString();
            stDEX.Text = dBonus.ToString();
            dBo.Text = dBonus.ToString();
            attackHit1.Text = "+" + (sBonus + probo).ToString();
        }
        private void updateCon(int coBonus)
        {
            int tmpdmg = maxHealth - currHealth;
            maxHealth = (8 * lvl) + (coBonus * lvl);
            if (currHealth == -1) currHealth = maxHealth;
            else currHealth = maxHealth - tmpdmg;
            updateHealth();
            stCON.Text = coBonus.ToString();
            coBo.Text = coBonus.ToString();
        }
        private void updateInt(int iBonus)
        {
            skillArcN.Text = iBonus.ToString();
            skillHisN.Text = iBonus.ToString();
            skillInvN.Text = iBonus.ToString();
            skillNatN.Text = iBonus.ToString();
            skillRelN.Text = iBonus.ToString();
            stINT.Text = iBonus.ToString();
            iBo.Text = iBonus.ToString();
            psInv.Text = skillInvN.Text;
        }
        private void updateWis(int wBonus)
        {
            skillAniN.Text = wBonus.ToString();
            skillInsN.Text = wBonus.ToString();
            skillMedN.Text = wBonus.ToString();
            skillPercN.Text = wBonus.ToString();
            skillSurN.Text = wBonus.ToString();
            stWIS.Text = wBonus.ToString();
            wBo.Text = wBonus.ToString();
            psPer.Text = skillPercN.Text;
            psIns.Text = skillInsN.Text;
        }
        private void updateCha(int chBonus)
        {
            skillDecN.Text = chBonus.ToString();
            skillIntN.Text = chBonus.ToString();
            skillPerfN.Text = chBonus.ToString();
            skillPersN.Text = chBonus.ToString();
            stCHA.Text = chBonus.ToString();
            chBo.Text = chBonus.ToString();
        }
        private void updateHealth()
        {
            HealthCM.Text = currHealth.ToString() + " / " + maxHealth.ToString();
        }

        private void LVL_TextChanged(object sender, EventArgs e)
        {
            isNum = int.TryParse(LVL.Text, out lvl);
            if (!isNum && LVL.Text != "")
            {
                LVL.Text = "1";
                lvl = 1;
            }
            else
            {
                probo = 2 + ((lvl - 1) / 4);
                ProBo.Text = "+" + probo.ToString();
            }
        }

        private void proAcr_CheckedChanged(object sender, EventArgs e)
        {
            AcrX = AcrX? false : true;
            skillAcrN.Text = AcrX ? (probo + dBonus).ToString() : dBonus.ToString();
        }
        private void proAni_CheckedChanged(object sender, EventArgs e)
        {
            AniX = AniX ? false : true;
            skillAniN.Text = AniX ? (probo + wBonus).ToString() : wBonus.ToString();
        }
        private void proArc_CheckedChanged(object sender, EventArgs e)
        {
            ArcX = ArcX ? false : true;
            skillArcN.Text = ArcX ? (probo + iBonus).ToString() : iBonus.ToString();
        }
        private void proAth_CheckedChanged(object sender, EventArgs e)
        {
            AthX = AthX ? false : true;
            skillAthN.Text = AthX ? (probo + sBonus).ToString() : sBonus.ToString();
        }
        private void proDec_CheckedChanged(object sender, EventArgs e)
        {
            DecX = DecX ? false : true;
            skillDecN.Text = DecX ? (probo + chBonus).ToString() : chBonus.ToString();
        }
        private void proHis_CheckedChanged(object sender, EventArgs e)
        {
            HisX = HisX ? false : true;
            skillHisN.Text = HisX ? (probo + iBonus).ToString() : iBonus.ToString();
        }
        private void proIns_CheckedChanged(object sender, EventArgs e)
        {
            InsX = InsX ? false : true;
            skillInsN.Text = InsX ? (probo + wBonus).ToString() : wBonus.ToString();
            psIns.Text = skillInsN.Text;
        }
        private void proInt_CheckedChanged(object sender, EventArgs e)
        {
            IntX = IntX ? false : true;
            skillIntN.Text = IntX ? (probo + chBonus).ToString() : chBonus.ToString();
        }
        private void proInv_CheckedChanged(object sender, EventArgs e)
        {
            InvX = InvX ? false : true;
            skillInvN.Text = InvX ? (probo + iBonus).ToString() : iBonus.ToString();
            psInv.Text = skillInvN.Text;
        }
        private void proMed_CheckedChanged(object sender, EventArgs e)
        {
            MedX = MedX ? false : true;
            skillMedN.Text = MedX ? (probo + wBonus).ToString() : wBonus.ToString();
        }
        private void proNat_CheckedChanged(object sender, EventArgs e)
        {
            NatX = NatX ? false : true;
            skillNatN.Text = NatX ? (probo + iBonus).ToString() : iBonus.ToString();
        }
        private void proPerc_CheckedChanged(object sender, EventArgs e)
        {
            PercX = PercX ? false : true;
            skillPercN.Text = PercX ? (probo + wBonus).ToString() : wBonus.ToString();
            psPer.Text = skillPercN.Text;
        }
        private void proPerf_CheckedChanged(object sender, EventArgs e)
        {
            PerfX = PerfX ? false : true;
            skillPerfN.Text = PerfX ? (probo + chBonus).ToString() : chBonus.ToString();
        }
        private void proPers_CheckedChanged(object sender, EventArgs e)
        {
            PersX = PersX ? false : true;
            skillPersN.Text = PersX ? (probo + chBonus).ToString() : chBonus.ToString();
        }
        private void proRel_CheckedChanged(object sender, EventArgs e)
        {
            RelX = RelX ? false : true;
            skillRelN.Text = RelX ? (probo + iBonus).ToString() : iBonus.ToString();
        }
        private void proSle_CheckedChanged(object sender, EventArgs e)
        {
            SleX = SleX ? false : true;
            skillSleN.Text = SleX ? (probo + dBonus).ToString() : dBonus.ToString();
        }
        private void proSte_CheckedChanged(object sender, EventArgs e)
        {
            SteX = SteX ? false : true;
            skillSteN.Text = SteX ? (probo + dBonus).ToString() : dBonus.ToString();
        }
        private void proSur_CheckedChanged(object sender, EventArgs e)
        {
            SurX = SurX ? false : true;
            skillSurN.Text = SurX ? (probo + wBonus).ToString() : wBonus.ToString();
        }
        private void proSTR_CheckedChanged(object sender, EventArgs e)
        {
            STRX = STRX ? false : true;
            stSTR.Text = STRX ? (probo + sBonus).ToString() : sBonus.ToString();
        }
        private void proDEX_CheckedChanged(object sender, EventArgs e)
        {
            DEXX = DEXX ? false : true;
            stDEX.Text = DEXX ? (probo + dBonus).ToString() : dBonus.ToString();
        }
        private void proCON_CheckedChanged(object sender, EventArgs e)
        {
            CONX = CONX ? false : true;
            stCON.Text = CONX ? (probo + coBonus).ToString() : coBonus.ToString();
        }
        private void proINT_CheckedChanged(object sender, EventArgs e)
        {
            INTELX = INTELX ? false : true;
            stINT.Text = INTELX ? (probo + iBonus).ToString() : iBonus.ToString();
        }
        private void proWIS_CheckedChanged(object sender, EventArgs e)
        {
            WISX = WISX ? false : true;
            stWIS.Text = WISX ? (probo + wBonus).ToString() : wBonus.ToString();
        }
        private void proCHA_CheckedChanged(object sender, EventArgs e)
        {
            CHAX = CHAX ? false : true;
            stCHA.Text = CHAX ? (probo + chBonus).ToString() : chBonus.ToString();
        }
        private void btnHeal_Click(object sender, EventArgs e)
        {
            isNum = int.TryParse(healthChange.Text, out healDmg);
            if (!isNum && healthChange.Text != "")
            {
                healthChange.Text = "";
                healDmg = 0;
            }
            else
            {
                currHealth += healDmg;
                if (currHealth > maxHealth) currHealth = maxHealth;
            }
            updateHealth();
        }
        private void btnDmg_Click(object sender, EventArgs e)
        {
            isNum = int.TryParse(healthChange.Text, out healDmg);
            if (!isNum && healthChange.Text != "")
            {
                healthChange.Text = "";
                healDmg = 0;
            }
            else if(currHealth == 0)
            {
                if (ds1.Checked == true) die();
                else
                {
                    ds1.Checked = true;
                    ds2.Checked = true;
                }
            }
            else {
                currHealth -= healDmg;
                if(0 - currHealth >= maxHealth / 2)
                {
                    die();
                }
                else if (currHealth < 0) currHealth = 0;
            }
            updateHealth();
        }
        private void ds1_Click(object sender, EventArgs e)
        {
            deathSave1 = deathSave1 ? false : true;
            if (deathSave1 && deathSave2 && deathSave3) HealthCM.Text = "DEAD";
        }
        private void ds2_Click(object sender, EventArgs e)
        {
            deathSave2 = deathSave2 ? false : true;
            if (deathSave1 && deathSave2 && deathSave3) HealthCM.Text = "DEAD";
        }
        private void ds3_Click(object sender, EventArgs e)
        {
            deathSave3 = deathSave3 ? false : true;
            if (deathSave1 && deathSave2 && deathSave3) HealthCM.Text = "DEAD";
        }
        private void die()
        {
            ds1.Checked = true;
            ds2.Checked = true;
            ds3.Checked = true;
            deathSave1 = true;
            deathSave2 = true;
            deathSave3 = true;
        }
        private void btnD4_Click(object sender, EventArgs e)
        {
            diceTotal = 0;
            isNum = int.TryParse(numD4.Text, out d4N);
            if (!isNum && numD4.Text != "")
            {
                numD4.Text = "";
                d4N = 0;
            }
            else
            {
                for (int i = 0; i < d4N; i++)
                {
                    diceTotal += rand.Next(4) + 1;
                }
            }
            txtDiceTotal.Text = diceTotal.ToString();
        }
        private void btnD6_Click(object sender, EventArgs e)
        {
            diceTotal = 0;
            isNum = int.TryParse(numD6.Text, out d6N);
            if (!isNum && numD6.Text != "")
            {
                numD6.Text = "";
                d6N = 0;
            }
            else
            {
                for (int i = 0; i < d6N; i++)
                {
                    diceTotal += rand.Next(6) + 1;
                }
            }
            txtDiceTotal.Text = diceTotal.ToString();
        }
        private void btnD8_Click(object sender, EventArgs e)
        {
            diceTotal = 0;
            isNum = int.TryParse(numD8.Text, out d8N);
            if (!isNum && numD8.Text != "")
            {
                numD8.Text = "";
                d8N = 0;
            }
            else
            {
                for (int i = 0; i < d8N; i++)
                {
                    diceTotal += rand.Next(8) + 1;
                }
            }
            txtDiceTotal.Text = diceTotal.ToString();
        }
        private void btnD12_Click(object sender, EventArgs e)
        {
            diceTotal = 0;
            isNum = int.TryParse(numD12.Text, out d12N);
            if (!isNum && numD12.Text != "")
            {
                numD12.Text = "";
                d12N = 0;
            }
            else
            {
                for (int i = 0; i < d12N; i++)
                {
                    diceTotal += rand.Next(12) + 1;
                }
            }
            txtDiceTotal.Text = diceTotal.ToString();
        }
        private void btnD20_Click(object sender, EventArgs e)
        {
            diceTotal = 0;
            isNum = int.TryParse(numD20.Text, out d20N);
            if (!isNum && numD20.Text != "")
            {
                numD20.Text = "";
                d20N = 0;
            }
            else
            {
                for (int i = 0; i < d20N; i++)
                {
                    diceTotal += rand.Next(20) + 1;
                }
            }
            txtDiceTotal.Text = diceTotal.ToString();
        }
        private void btnPercentage_Click(object sender, EventArgs e)
        {
            diceTotal = rand.Next(100) + 1;
            txtDiceTotal.Text = diceTotal.ToString();
        }
        private void CharSheet_FormClosing(object sender, FormClosingEventArgs e)
        {
            TextWriter tw = new StreamWriter("SavedSheet.txt");
            tw.WriteLine(charName);
            tw.WriteLine(lvl);
            tw.WriteLine(insanity);
            tw.WriteLine(currHealth);
            tw.WriteLine(maxHealth);
            tw.WriteLine(str);
            tw.WriteLine(dex);
            tw.WriteLine(con);
            tw.WriteLine(intel);
            tw.WriteLine(wis);
            tw.WriteLine(cha);
            tw.Close();
            TextWriter tw1 = new StreamWriter("SheetNotes.txt");
            tw1.Write(txtNotes.Text);
            tw1.Close();
        }
    }
}
