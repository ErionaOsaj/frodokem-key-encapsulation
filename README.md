# FrodoKEM-key-encapsulation

## Team Members
- Eriona Osaj
- Sadik Zenuni
  
## Introduction

Two people could agree on a secret value if one of them could send the secret in an encrypted form to the other one, such that only the other one could decrypt and use it. This is what a KEM makes possible, through a collection of three algorithms:

- A key generation algorithm, Generate, which generates a public key and a private key (a keypair).

- An encapsulation algorithm, Encapsulate, which takes as input a public key, and outputs a shared secret value and an “encapsulation” (a ciphertext) of this secret value.

- A decapsulation algorithm, Decapsulate, which takes as input the encapsulation and the private key, and outputs the shared secret value.

## Requirements
- .NET Core 3.1 or higher
- Visual Studio 2019 or newer (for development)


## Security Levels

FrodoKEM is designed to offer different levels of security to meet various application needs. The levels are based on the estimated computational effort required to break the encryption, analogous to the effort required to break symmetric key encryption of a certain bit length:

- **FrodoKEM-640**: Targets security equivalent to AES-128. Recommended for general applications requiring good security against quantum attacks.
- **FrodoKEM-976**: Targets security equivalent to AES-192. Suitable for higher security requirements.
- **FrodoKEM-1344**: Targets security equivalent to AES-256. Designed for scenarios demanding the highest level of security against future quantum computers.

Each level reflects a trade-off between performance and security: higher levels offer greater security but may have a performance impact. It is essential to choose the appropriate level based on the specific security requirements of your application.
