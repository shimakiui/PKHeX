﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PKHeX.Core;

namespace PKHeX.Tests.Saves
{
    [TestClass]
    public class MemeCryptoTests
    {
        const string TestCategory = "MemeCrypto Tests";

        [TestMethod]
        [TestCategory(TestCategory)]
        public void TestVerifySaveSignature()
        {
            var savebuffer =
                "58EA53A7133F34DA9F2BEC12F1560354E8BDF8A484ADE4E2954D3C48673118EB67E2D52ED0196E54DC5D93013E9F3B00C8A43B556AEE8C2F763EA9DC125988C6B5F2D3C74CA2C58026BB024B403D09BC5950C54CEB6F21E45D0B66B68791BCBB6D7E67C2F7E4A7F4A517FC50B4FEED9A65BF901ABEB0FFAC44AE07237BE5DD2D"
                    .ToByteArray();
            Assert.IsTrue(MemeCrypto.VerifyMemeData(savebuffer, out var memebuffer));
        }

        [TestMethod]
        [TestCategory(TestCategory)]
        public void TestVerifyMemeBuffer()
        {
            var encrypted =
                "000102030405060708090A0B0C0D0E0F101112131415161718191A1B1C1D1E1F202122232425262728292A2B2C2D2E2F303132333435363738393A3B3C3D3E3F404142434445464748494A4B4C4D4E4F505152535455565758595A5B5C5D5E5F606162636465666768696A6B6C6D6E6F707172737475767778797A7B7C7D7E7F808182838485868788898A8B8C8D8E8F909192939495969798999A9B9C9D9E9F4185F2713D5A8BCFFBFF47F86867DF680354913FEFEF08D0D0F1054CB054B508E7DE3AA789AC444D087AB9412CEE965A6D38E45EA010C77FF174F8F8D9993AEAFB9C0616E287B74487E0FF3F1874291B8170C71113971752717F8D188A319EA6"
                    .ToByteArray();
            var decrypted =
                "000102030405060708090A0B0C0D0E0F101112131415161718191A1B1C1D1E1F202122232425262728292A2B2C2D2E2F303132333435363738393A3B3C3D3E3F404142434445464748494A4B4C4D4E4F505152535455565758595A5B5C5D5E5F606162636465666768696A6B6C6D6E6F707172737475767778797A7B7C7D7E7F808182838485868788898A8B8C8D8E8F909192939495969798999A9B9C9D9E9FA0A1A2A3A4A5A6A7A8A9AAABACADAEAFB0B1B2B3B4B5B6B7B8B9BABBBCBDBEBFC0C1C2C3C4C5C6C7C8C9CACBCCCDCECFD0D1D2D3D4D5D6D7D8D9DADBDCDDDEDFE0E1E2E3E4E5E6E7E8E9EAEBECEDEEEFF0F1F2F3F4F5F6F7431E10EF7681217C"
                    .ToByteArray();

            Assert.IsTrue(MemeCrypto.VerifyMemeData(encrypted, out var actual, MemeKeyIndex.PokedexAndSaveFile));
            for (var i = 0; i < decrypted.Length; i++)
            {
                Assert.AreEqual(decrypted[i], actual[i]);
            }
        }


        [TestMethod]
        [TestCategory(TestCategory)]
        public void TestSignMemeBuffer()
        {
            var encrypted =
                "000102030405060708090A0B0C0D0E0F101112131415161718191A1B1C1D1E1F202122232425262728292A2B2C2D2E2F303132333435363738393A3B3C3D3E3F404142434445464748494A4B4C4D4E4F505152535455565758595A5B5C5D5E5F606162636465666768696A6B6C6D6E6F707172737475767778797A7B7C7D7E7F808182838485868788898A8B8C8D8E8F909192939495969798999A9B9C9D9E9F4185F2713D5A8BCFFBFF47F86867DF680354913FEFEF08D0D0F1054CB054B508E7DE3AA789AC444D087AB9412CEE965A6D38E45EA010C77FF174F8F8D9993AEAFB9C0616E287B74487E0FF3F1874291B8170C71113971752717F8D188A319EA6"
                    .ToByteArray();
            var decrypted =
                "000102030405060708090A0B0C0D0E0F101112131415161718191A1B1C1D1E1F202122232425262728292A2B2C2D2E2F303132333435363738393A3B3C3D3E3F404142434445464748494A4B4C4D4E4F505152535455565758595A5B5C5D5E5F606162636465666768696A6B6C6D6E6F707172737475767778797A7B7C7D7E7F808182838485868788898A8B8C8D8E8F909192939495969798999A9B9C9D9E9FA0A1A2A3A4A5A6A7A8A9AAABACADAEAFB0B1B2B3B4B5B6B7B8B9BABBBCBDBEBFC0C1C2C3C4C5C6C7C8C9CACBCCCDCECFD0D1D2D3D4D5D6D7D8D9DADBDCDDDEDFE0E1E2E3E4E5E6E7E8E9EAEBECEDEEEFF0F1F2F3F4F5F6F7F8F9FAFBFCFDFEFF"
                    .ToByteArray();

            var actual = MemeCrypto.SignMemeData(decrypted);
            for (var i = 0; i < encrypted.Length; i++)
            {
                Assert.AreEqual(encrypted[i], actual[i]);
            }
        }

        [TestMethod]
        [TestCategory(TestCategory)]
        public void TestVerifyMemeBufferOffsetLength()
        {
            var encrypted =
                "CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC000102030405060708090A0B0C0D0E0F101112131415161718191A1B1C1D1E1F202122232425262728292A2B2C2D2E2F303132333435363738393A3B3C3D3E3F404142434445464748494A4B4C4D4E4F505152535455565758595A5B5C5D5E5F606162636465666768696A6B6C6D6E6F707172737475767778797A7B7C7D7E7F808182838485868788898A8B8C8D8E8F909192939495969798999A9B9C9D9E9F4185F2713D5A8BCFFBFF47F86867DF680354913FEFEF08D0D0F1054CB054B508E7DE3AA789AC444D087AB9412CEE965A6D38E45EA010C77FF174F8F8D9993AEAFB9C0616E287B74487E0FF3F1874291B8170C71113971752717F8D188A319EA6CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC"
                    .ToByteArray();
            var decrypted =
                "CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC000102030405060708090A0B0C0D0E0F101112131415161718191A1B1C1D1E1F202122232425262728292A2B2C2D2E2F303132333435363738393A3B3C3D3E3F404142434445464748494A4B4C4D4E4F505152535455565758595A5B5C5D5E5F606162636465666768696A6B6C6D6E6F707172737475767778797A7B7C7D7E7F808182838485868788898A8B8C8D8E8F909192939495969798999A9B9C9D9E9FA0A1A2A3A4A5A6A7A8A9AAABACADAEAFB0B1B2B3B4B5B6B7B8B9BABBBCBDBEBFC0C1C2C3C4C5C6C7C8C9CACBCCCDCECFD0D1D2D3D4D5D6D7D8D9DADBDCDDDEDFE0E1E2E3E4E5E6E7E8E9EAEBECEDEEEFF0F1F2F3F4F5F6F7431E10EF7681217CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC"
                    .ToByteArray();

            Assert.IsTrue(MemeCrypto.VerifyMemeData(encrypted, out var actual, 0x10, 0x100,
                MemeKeyIndex.PokedexAndSaveFile));
            for (var i = 0; i < decrypted.Length; i++)
            {
                Assert.AreEqual(decrypted[i], actual[i]);
            }
        }

        [TestMethod]
        [TestCategory(TestCategory)]
        public void TestVerifyPoke()
        {
            var vector =
                "A96E2D8D9B99DBFB934939C097E3AC101C7D48CEC52FCA717B14B19890208592045C430035DD09A31446142E9EA33CF3E6B6E69484B6D2EED500B8389048013491602403DBE7B814EA069667CFADAFE74895217D78037B4A456FAB2CAFD71E69504F4B4509000000000000"
                    .ToByteArray();
            var vector2 =
                "A96E2D8D9B99DBFB934939C097E3AC101C7D48CEC52FCA717B14B19890208592045C430035DD09A31446142E9EA33CF3E6B6E69484B6D2EED500B8389048013491602403DBE7B814EA069667CFADAFE74895217D78037B4A456FAB2CAFD71E690000504F4B4509000000000000"
                    .ToByteArray();

            Assert.IsTrue(MemeCrypto.VerifyMemePOKE(vector, out var output));
            Assert.IsTrue(MemeCrypto.VerifyMemePOKE(vector2, out var output2));

        }

        [TestMethod]
        [TestCategory(TestCategory)]
        public void TestVerifyKnownKeys()
        {
            var vectors = new Dictionary<MemeKeyIndex, byte[]>
            {
                {MemeKeyIndex.LocalWireless, "1010030000030000000A0070300051308B3001FFD43020AB301003B430FC30B90830C830D10007C630A382000D6E300A005F00055700305D3046306A300000307530933044304D28306B000B5500158F308C203066002581305A3089A0002744002344308D30612A304C00276E0031DF0001C30830AD30E500110A0042A0004973003D4D305F308880007B0A006830823060C4002F10516A3063004D42305F5200536D009900A6F002F014F026FFF038F04AF05CF06EF080F092F0A4F0B6FFF0C8F0DAF0ECF0FEF110F122F134F1460000B5E400004646F86000F1AB31AB0030D730EC0E30BC30F3020FF173A723C162C9700608C5DCAFAB898032382AF1FA717BBC5D91C61AD7B6EA356220742C2DD4595765FCD8123D1E10ED6B0A1DA45D0C2148035A560BB1C5C246FA7EB64D729FC1DD8B61B4737DC51E184A97E4795E82BFD045743EC2AEAED26E87".ToByteArray()},
                {MemeKeyIndex.FriendlyCompetition, "0300000077000000000000000D0106060404003204000101000000000000000000000000000000000000C00000000000000000000000000E000000000000000000000000000000C0070000000000000000000000987E000000000000000000000000000000000000D80300000000000000F00300000000000000E001070000000000000000000000000000000000000000000000000000000101920200000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000001E2D0100015A03000000BE0700000000C287010050006F006B00E9006D006F006E00200054007200610069006E006500720020004F006E006C0069006E006500200043006F006D007000650074006900740069006F006E000000000000007700770077002E0070006F006B0065006D006F006E002D0074007200610069007DC67F945B27557C97D6C0B6D20FF11AFCE1580C5E9E80C7C3FBBA53B67F24D1A91BA2FB8CDD31F7FE29D894B3764A45407658BB9A6E6DC8B3F6A45F77686AC56EA64C0298D1B22F3D3850ADE10AEA6E185FD0EEA91AA38E3F0F0AA9E7ADA132".ToByteArray()},
                {MemeKeyIndex.LiveCompetition, "0300000077000000000000000D0006060303003204000101000000000000000000000000000000000000C00000000000000000000000000E000000000000000000000000000000C0070000000000000000000000987E000000000000000000000000000000000000D80300000000000000F00300000000000000E001070000000000000000000000000000000000000000000000000000000101920200000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000001E2D0100015A03000000BE07000000000B9101004900720069007300200043006D007000650074006900740069006F006E00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000066007200650065000000000000000000000000000000000000000000000000006425CA0A78E79154EBA19550A0734F84A0C49F8B02E51FA8310548FFF14B7338276188C11C85C15701DAED228099FBF95A277E6249625208D8992FF027385D897AC097F0240C93A70E35FB8AD971DFE14832988DC70F5D1BE19C7EF5FBAFCC12".ToByteArray()},
                {MemeKeyIndex.RentalTeam, "0300000077000000B8B9BABBBCBDBEBFC0C1C2C3C4C5C6C74E35E4783A7E9E161C919C0E736F61B31741B8C1873C5D06D1D408D9DA9FE28DCB53851518FCC0758915EE2911B2194B20131C2E87BC63A255F3A5187763FAF99B1B99D0E4460125CCB7F1CE4A3CC0FC64463454DB25CC851EF9A360EA3D4BDB44B8DE96D1C9839744A3BF7A27FDF41A86491666DAA7BEF55EBFB954BA3FF741657252490BD11350EEF367DA0D229E04EE61DCA1220576014AC2E24232337AB4E7ADEE92B59C44E9AB8D19E0BA87547A0351C4088EDF8808D692461C84EB5D07C795D3B037290AC7C5A9FD2775A86EAD628F545F6E5193145ED22B3BCDE16FD3CCEEA80A5FD90CF8FD320C1D064C8881BF369F0D82EE0E637C434EC6CA4221FE0952C28A7E930956DC2B990817BCC2349EC4339CDD09E2ABB94DC44C171A1E70E2CC5538B9686A1E4AFF907436378FE2915F217EC7C3BA69E47C913794FFE8285DD399D3E39154CE51BA0C74663BD685634697D76EAB8890CF77E371BA7E5CD5FA78FAC7D58A3843C993331C8AFC5ABDCD17FC42B7C28A196C2FB0EF03FE84CAF3FD626A584D9406BC6FC5DC20DD0E19D1D08C96D9065F376E052639FFC0F61CCCE84CDC833F31B9A6C830DE9F58B257F5CBE5991FCA51871752B611AE3B21F5E1F1AE3CFAB49BC6F6721C1C18B74B11400D029E268D".ToByteArray()},
                {MemeKeyIndex.PokedexAndSaveFile, "4F3994147824D4F9D9B3EEBDC2DA6777585413FA41D22347A3593E22467E2F117B51EDFEAE5977E19AD88EB1C63ADB7E9DEDB7F5AEEFECA5F777C7F12527729025C82DC4A12C8B344EB472B9F68BFD999916F0D03460FF1D3FDC5EF0F7A7F8FE".ToByteArray()},
                {MemeKeyIndex.GaOle, "7C7F70413806DFD1D4E8F5610BE3CA705245593C24AB3102D7768D43B3D873EEB5BB6B188FD4F38F06EB06C1A27369E9822D10093DAD17692EA2D71BCB8941F883F3F0E2068941F65FB26950B7E54F5990E163809594381C99CF3330F052452C".ToByteArray()},
                {MemeKeyIndex.MagearnaEvent, "9D741CBA2ABA23408461AC168B3227199C15FFA2784662FE169459DDC2409AE08FA302A6E7026051C8378D09898500028021FE21B1A8165C9918779BA140597E03ED3EAF2FD82C026B17E6BB0D1C4BC6CE06E9FAC2E5DCDD4025983DE57C67CA".ToByteArray()},
                {MemeKeyIndex.MoncolleGet, "9914945B3280796FF8EC694681ED2218F8FCD1C6628FCFACD633A86622D278C09FD1BA1B2E60AF2896EDDAF76110CA246AED9E08D7D90F7F285764BBD3E20E05A11BA214C96B1A54E1A8D48DE6EF060C79011A4B3326FBE55F29FDE40B31F470".ToByteArray()},
                {MemeKeyIndex.IslandScanEventSpecial, "34B08442E3D68D3730CF5CEE9E82BCFD5F6664168F43E9F2EA26CC7B94B1F44FF7B3C0BAD5C346693DDAD5461E6087AD061015744F6DBA294F9BCD6F0E7A35859F0A15F358EB00BED5A4C18841A8B6A69D91FC603E6DCE058DD96983C5DFBB04".ToByteArray()},
                {MemeKeyIndex.TvTokyoDataBroadcasting, "195B4EA9F48E6ADBEE73EB0D529C14EDC99BB011F214E0812E5198BE40739783646FB8C75ABFFD84AA11FB095EFFF5E383F7F45A56A96780C79EBABB430D860FA6CB3D0BFC163C52EDC5C199963D3E518758F911569E1C667BF8A15643A19E2F".ToByteArray()},
                {MemeKeyIndex.CapPikachuEvent, "A96E2D8D9B99DBFB934939C097E3AC101C7D48CEC52FCA717B14B19890208592045C430035DD09A31446142E9EA33CF3E6B6E69484B6D2EED500B8389048013491602403DBE7B814EA069667CFADAFE74895217D78037B4A456FAB2CAFD71E69".ToByteArray()},
                {MemeKeyIndex.Unknown10, null},
                {MemeKeyIndex.Unknown11, null},
                {MemeKeyIndex.Unknown12, null},
                {MemeKeyIndex.Unknown13, null}

            };

            foreach (var keyIndex in vectors.Keys)
            {
                if (vectors[keyIndex] != null)
                {
                    Assert.IsTrue(MemeCrypto.VerifyMemeData(vectors[keyIndex], out var signed, keyIndex), $"Failed to verify test vector for MemeKey {keyIndex}!");
                }
            }
        }
    }
}