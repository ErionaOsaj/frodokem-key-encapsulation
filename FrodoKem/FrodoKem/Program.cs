//// See https://aka.ms/new-console-template for more information

using FrodoKem.Utils;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Pqc.Crypto.Frodo;
using Org.BouncyCastle.Utilities.Encoders;


Dictionary<string, FrodoParameters> Parameters = new Dictionary<string, FrodoParameters>()
        {
           
            { "PQCkemKAT_43088_shake.rsp", FrodoParameters.frodokem43088shaker3 },
        };


var name = "PQCkemKAT_43088_shake.rsp";
var frodoParameters = Parameters[name];


var strseed = "061550234D158C5EC95595FE04EF7A25767F2E24CC2BC479D09D86DC9ABCFDE7056A8C266F9EF97ED08541DBD2E1FFA1";
//var strseed = "061550234D158C5EC95595FE04EF7A25767F2E24CC2BC479D09D86DC9ABCFDE7056A8C266F9EF97ED08541DBD2E1FFA1";


byte[] seed = Hex.Decode(strseed);
FrodoKeyPairGenerator kpGen = new FrodoKeyPairGenerator();
NistSecureRandom random = new NistSecureRandom(seed, null);

FrodoKeyGenerationParameters genParams = new FrodoKeyGenerationParameters(random, frodoParameters);

kpGen.Init(genParams);
AsymmetricCipherKeyPair kp = kpGen.GenerateKeyPair();
FrodoPublicKeyParameters pubParams = (FrodoPublicKeyParameters)kp.Public;
FrodoPrivateKeyParameters privParams = (FrodoPrivateKeyParameters)kp.Private;


FrodoKEMGenerator frodoEncCipher = new FrodoKEMGenerator(random);
ISecretWithEncapsulation secWenc = frodoEncCipher.GenerateEncapsulated(pubParams);
byte[] generated_cipher_text = secWenc.GetEncapsulation();
byte[] original_secret = secWenc.GetSecret();

//  Decapsulation
FrodoKEMExtractor frodoDecCipher = new FrodoKEMExtractor(privParams);
byte[] decapsulated_secret = frodoDecCipher.ExtractSecret(generated_cipher_text);

// Test to ensure they are equal
bool isSuccess = original_secret.SequenceEqual(decapsulated_secret);

Console.WriteLine($"Encapsulation/Decapsulation Test: {(isSuccess ? "Success" : "Failure")}");


Console.WriteLine("Press any key to continue...");
Console.ReadKey();

