# Infromation Security Assignmnent - FrodoKEM : Learning with errors key encapsulation in .Net

## Team Members
- Eriona Osaj
- Sadik Zenuni
  
## Introduction
The most widely used key exchange algorithms today are based on hard mathematical problems, such as integer factorization and the discrete logarithm problem. But these problems can be efficiently solved by a quantum computer, as we have previously learned, breaking the secrecy of the communication.

There are other mathematical problems that are hard even for quantum computers to solve, such as those based on lattices or isogenies. These problems can be used to build key exchange algorithms that are secure even in the face of quantum computers. Before we dive into this matter, we have to first look at one algorithm that can be used for Key Exchange: Key Encapsulation Mechanisms (KEMs).
Two people could agree on a secret value if one of them could send the secret in an encrypted form to the other one, such that only the other one could decrypt and use it. This is what a KEM makes possible, through a collection of three algorithms:

- A key generation algorithm, Generate, which generates a public key and a private key (a keypair).

- An encapsulation algorithm, Encapsulate, which takes as input a public key, and outputs a shared secret value and an “encapsulation” (a ciphertext) of this secret value.

- A decapsulation algorithm, Decapsulate, which takes as input the encapsulation and the private key, and outputs the shared secret value.

## Requirements
- .NET Core 6
- Visual Studio 2019 or newer (for development)
## Code implementation
Algorithm described above was implemented using BouncyCastle.Cryptography library.
<img width="586" alt="image" src="https://github.com/ErionaOsaj/frodokem-key-encapsulation/assets/27639068/5c5cae69-d9e1-4765-9ef5-e56956c4649f">

If we run code we can see encapsulation/decapsulation of key was successful.
<img width="321" alt="image" src="https://github.com/ErionaOsaj/frodokem-key-encapsulation/assets/27639068/8348ff04-9202-4361-aa4d-1a529a003dea">

## Security Levels

FrodoKEM is designed to offer different levels of security to meet various application needs. The levels are based on the estimated computational effort required to break the encryption, analogous to the effort required to break symmetric key encryption of a certain bit length:

- **FrodoKEM-640**: Targets security equivalent to AES-128. Recommended for general applications requiring good security against quantum attacks.
- **FrodoKEM-976**: Targets security equivalent to AES-192. Suitable for higher security requirements.
- **FrodoKEM-1344**: Targets security equivalent to AES-256. Designed for scenarios demanding the highest level of security against future quantum computers.

Each level reflects a trade-off between performance and security: higher levels offer greater security but may have a performance impact. It is essential to choose the appropriate level based on the specific security requirements of your application.

## Additional Resources
- [Library used in code](https://www.nuget.org/packages/BouncyCastle.Cryptography/2.2.1)
- [Algorithm Specifications And Supporting Documentation](https://frodokem.org/files/FrodoKEM-specification-20171130.pdf)

## References
- [Deep dive into a post-quantum key encapsulation algorithm](https://blog.cloudflare.com/post-quantum-key-encapsulation/)

