# [0.39.0](https://github.com/TomaszKandula/TokanPages/compare/v0.38.0...v0.39.0) (2022-01-09)


### Features

* add controller with endpoints for log files ([ee3f61e](https://github.com/TomaszKandula/TokanPages/commit/ee3f61e10345e33a4951b37614ed12d875748e79))
* add cqrs for getting assets and article assets ([8a7b908](https://github.com/TomaszKandula/TokanPages/commit/8a7b908720f378a97dc575577dec908d28414f79))
* add cqrs to handle log files ([8154f4c](https://github.com/TomaszKandula/TokanPages/commit/8154f4c2ab51809b98c0ebb0b748707946bbb116))
* add new endpoint to get assets list ([48e7121](https://github.com/TomaszKandula/TokanPages/commit/48e71211b2abf56631f450ad361e52fc3596efa2))
* add new error code ([91cae89](https://github.com/TomaszKandula/TokanPages/commit/91cae89a51e3420bd855d50be46f8e08094cd86a))
* add new error code ([2c9582b](https://github.com/TomaszKandula/TokanPages/commit/2c9582b5da50d96a1e68b3740084467a845d782c))
* add new validation and error codes ([ab5e01e](https://github.com/TomaszKandula/TokanPages/commit/ab5e01e999488b9ddfd19785d98c1685a35f7823))
* change endpoint route ([9f9b1bd](https://github.com/TomaszKandula/TokanPages/commit/9f9b1bdfce6d6383e3c9b19a99c6742346a26a73))
* change logger configuration, allow to log to the file ([df0df70](https://github.com/TomaszKandula/TokanPages/commit/df0df7074b7e1d90b2d9cc9b85dcb727f24581b4))
* improve Azure Storage implementations ([841c078](https://github.com/TomaszKandula/TokanPages/commit/841c07872427308d411f6bd72bfc9553937b655e))

# [0.38.0](https://github.com/TomaszKandula/TokanPages/compare/v0.37.0...v0.38.0) (2022-01-07)


### Features

* add new service for email sending ([fc3647a](https://github.com/TomaszKandula/TokanPages/commit/fc3647a6d3e1d76b18f98a4409e22989dbc23b34))
* add saving default user avatar from predefined image kept on Azure Storage ([12ed015](https://github.com/TomaszKandula/TokanPages/commit/12ed0151500bb9d29a9e630f51c7baf25f69ab00))
* change behaviour from return null to thorw exception ([ac30ae3](https://github.com/TomaszKandula/TokanPages/commit/ac30ae3e80ee2fe83baf699c90176bfe0b446abf))
* improve Azure Blob Storage implementation, add new method ([fc64c8c](https://github.com/TomaszKandula/TokanPages/commit/fc64c8cf42ef2a281ab31d0fb245f7a806ecda2f))
* register email sender service ([2734c82](https://github.com/TomaszKandula/TokanPages/commit/2734c829c06b67fbb5708797e3238493ff6a9903))
* update implementation, use email sender service to simplify code ([5b625c8](https://github.com/TomaszKandula/TokanPages/commit/5b625c8bd76026f09194f39651659b3e61d70850))

# [0.37.0](https://github.com/TomaszKandula/TokanPages/compare/v0.36.0...v0.37.0) (2022-01-07)


### Features

* add new string extension to generate from template ([3c47a91](https://github.com/TomaszKandula/TokanPages/commit/3c47a913055e0f9224a3829fbbb483ec001022cf))
* add web token validation implementation in the service ([1a8c067](https://github.com/TomaszKandula/TokanPages/commit/1a8c067de8ba7ddfc0c91e51304ad707b588ad7b))
* register IWebTokenValidation ([ac56bc7](https://github.com/TomaszKandula/TokanPages/commit/ac56bc738c8b09b8a91c0f911089c6b6f99df6eb))
* use implementation from newly added service ([44243ab](https://github.com/TomaszKandula/TokanPages/commit/44243ab02bf2b663c7b7675bfbfe2b7c07c00417))

# [0.36.0](https://github.com/TomaszKandula/TokanPages/compare/v0.35.0...v0.36.0) (2022-01-02)


### Bug Fixes

* correct coverage report path ([b5e5a8f](https://github.com/TomaszKandula/TokanPages/commit/b5e5a8f0c597ec9d11caf9e987b4c90667c72506))
* remove aggregate exception ([ad3e07f](https://github.com/TomaszKandula/TokanPages/commit/ad3e07fab49928100c918881e3932f80ba44b765))
* remove old reference ([4014669](https://github.com/TomaszKandula/TokanPages/commit/40146697bde265301e54f7a23dd1dba426dac32a))


### Features

* add behaviour service, update references ([2e3b4a7](https://github.com/TomaszKandula/TokanPages/commit/2e3b4a74e9d858732fb6b8a6d80880460691ac5a))
* add behaviours services ([ece9f0b](https://github.com/TomaszKandula/TokanPages/commit/ece9f0bc8865815ee912a550988f872f5d57ab14))
* allow only GET and POST methods ([9b68d1b](https://github.com/TomaszKandula/TokanPages/commit/9b68d1be79d741c51196de40f3bb229c3a3b67ea))
* register web token check behaviour ([438aabf](https://github.com/TomaszKandula/TokanPages/commit/438aabf50d9c4bd56878f7285350e7ae014a0ed2))

# [0.35.0](https://github.com/TomaszKandula/TokanPages/compare/v0.34.0...v0.35.0) (2022-01-02)


### Features

* add optional headers to custom http client configuration ([44ab49d](https://github.com/TomaszKandula/TokanPages/commit/44ab49d22786f3995f5a6d3a20b4f067a063fecb))
* remove private key property from model ([ed1a429](https://github.com/TomaszKandula/TokanPages/commit/ed1a429f81f0ebb8fc0c33249cb400a158a4cbc4))
* use private key in request header ([5d7461f](https://github.com/TomaszKandula/TokanPages/commit/5d7461fdeb043f0ee064aed09249299f44480519))

# [0.34.0](https://github.com/TomaszKandula/TokanPages/compare/v0.33.0...v0.34.0) (2021-12-27)


### Bug Fixes

* change exception type from System.Exception to BusinessException ([36a55af](https://github.com/TomaszKandula/TokanPages/commit/36a55af99c50e105acf0f33ac0130d17dde38b77))
* change exception type, add logger ([bcb3520](https://github.com/TomaszKandula/TokanPages/commit/bcb3520f8b9b18a27a13805cf142a0dd7909c039))


### Features

* add new error code ([b93032a](https://github.com/TomaszKandula/TokanPages/commit/b93032a685e6baa7840f23126bdf8ae8bb4c9e04))

# [0.33.0](https://github.com/TomaszKandula/TokanPages/compare/v0.32.0...v0.33.0) (2021-12-20)


### Features

* switch language version to 10, disable publishing ([ba1a3d6](https://github.com/TomaszKandula/TokanPages/commit/ba1a3d6483c7bf36aa05e44df3b67a2aa4158c1a))
* switch to C# 10 ([38fbecc](https://github.com/TomaszKandula/TokanPages/commit/38fbecc883643e71cb889b44d810041a50a22b46))

# [0.32.0](https://github.com/TomaszKandula/TokanPages/compare/v0.31.0...v0.32.0) (2021-12-20)


### Features

* after migration to NET6, set user secrets to be optional explicite ([9b163cd](https://github.com/TomaszKandula/TokanPages/commit/9b163cd9387e737a2099338aeaeb5ff11320fba7))
* upgrade to NET6, upgrade all libraries ([ca4ebef](https://github.com/TomaszKandula/TokanPages/commit/ca4ebefbfad583926538a70eae224df91272a479))

# [0.31.0](https://github.com/TomaszKandula/TokanPages/compare/v0.30.1...v0.31.0) (2021-12-05)


### Features

* add api version in configuration/build workflow, docker ([bceb67c](https://github.com/TomaszKandula/TokanPages/commit/bceb67c91ea44be39214aa20b118b27f2cb851ce))
* add api versioning to all controllers ([8383af4](https://github.com/TomaszKandula/TokanPages/commit/8383af4ede9d150c43c6ff58fd460867593dc308))
* add error code for invalid api version ([6aac3a6](https://github.com/TomaszKandula/TokanPages/commit/6aac3a6bf9b73f5e53894e9b2a0561e5e0789314))
* add new Api version exception ([45d1705](https://github.com/TomaszKandula/TokanPages/commit/45d170532738fe15c8196a28e7381a697baba28b))
* configure startup fpr api versioning ([61fa9ba](https://github.com/TomaszKandula/TokanPages/commit/61fa9babe1d9ae22401bac9c2b1742aa53c94314))

## [0.30.1](https://github.com/TomaszKandula/TokanPages/compare/v0.30.0...v0.30.1) (2021-12-04)


### Bug Fixes

* change Redis Cache key to be unique for different content request ([53dde19](https://github.com/TomaszKandula/TokanPages/commit/53dde19233815c4db271d6f0a2f586bd617f7e86))

# [0.30.0](https://github.com/TomaszKandula/TokanPages/compare/v0.29.1...v0.30.0) (2021-12-04)


### Features

* add new Error Code for invalid argument ([90866e2](https://github.com/TomaszKandula/TokanPages/commit/90866e258f684ccd5154876c5f3d9c9daefe7aa6))
* add Redis Cache configuration to application settings ([c9ad5fa](https://github.com/TomaszKandula/TokanPages/commit/c9ad5fa0cdc620d82c75f8611fcc4f4917d26ac2))
* add Redis Cache extension package ([7c9e337](https://github.com/TomaszKandula/TokanPages/commit/7c9e3377e7a7359930b3527737515c4015fc933c))
* add Redis Cache implementation to services ([c6a8efb](https://github.com/TomaszKandula/TokanPages/commit/c6a8efb93dd88c1a31b091f8007c6dfd0c812373))
* add services for caching the endpoints result content ([c75ab8d](https://github.com/TomaszKandula/TokanPages/commit/c75ab8d25eff2b2dec6e6660960062ae6eeeaf87))
* register new services for Redis Cache ([a071167](https://github.com/TomaszKandula/TokanPages/commit/a07116700c4159aadfd9ac11363292b0815c13ec))
* relocate content properties to separate class ([738261b](https://github.com/TomaszKandula/TokanPages/commit/738261b9bbe514baa6f00fc75750eaefcb3b8142))
* use caching services for GET endpoints ([51f317c](https://github.com/TomaszKandula/TokanPages/commit/51f317c79a56ecec42f119638209bfa0b8d78921))

## [0.29.1](https://github.com/TomaszKandula/TokanPages/compare/v0.29.0...v0.29.1) (2021-11-30)


### Bug Fixes

* change expected exception types ([89a7bc5](https://github.com/TomaszKandula/TokanPages/commit/89a7bc5405bdf081cc5b8f4e6c37077f71805f25))

# [0.29.0](https://github.com/TomaszKandula/TokanPages/compare/v0.28.0...v0.29.0) (2021-11-30)


### Features

* add error codes ([c088b9b](https://github.com/TomaszKandula/TokanPages/commit/c088b9b4b8dd5a1df165927a7b548dd1561c6b84))
* add extension method for IEnumerable ([9410e45](https://github.com/TomaszKandula/TokanPages/commit/9410e45b95c5046cf96d179d7a4cd2def26286dd))
* add methods to deal with user token ([ddf0964](https://github.com/TomaszKandula/TokanPages/commit/ddf09643788f8d2772d64fbc9ec109c2b524eed6))

# [0.28.0](https://github.com/TomaszKandula/TokanPages/compare/v0.27.1...v0.28.0) (2021-11-20)


### Features

* update startup configuration to use extension methods ([1900632](https://github.com/TomaszKandula/TokanPages/commit/190063246bd01e076e0c3ad504f35814eee03742))
* upgrade implementation for cors and swagger configurations ([70b3482](https://github.com/TomaszKandula/TokanPages/commit/70b3482b3c55fb5778a3bb92c16792d39ee27326))

## [0.27.1](https://github.com/TomaszKandula/TokanPages/compare/v0.27.0...v0.27.1) (2021-10-31)


### Bug Fixes

* remove whitespace ([17dd22e](https://github.com/TomaszKandula/TokanPages/commit/17dd22e17a6c7935f5cb2ab7b0575fd4eff40819))

# [0.27.0](https://github.com/TomaszKandula/TokanPages/compare/v0.26.0...v0.27.0) (2021-10-31)


### Bug Fixes

* remove ETag filter to solve retrieval issues ([188fbf1](https://github.com/TomaszKandula/TokanPages/commit/188fbf1bcf603ad0d955728f6c4481906dd0c7f4))


### Features

* move article asset endpoint to assets controller ([f492536](https://github.com/TomaszKandula/TokanPages/commit/f49253635cbc0f953f3d7c4d1258622aa172dc49))

# [0.26.0](https://github.com/TomaszKandula/TokanPages/compare/v0.25.0...v0.26.0) (2021-10-31)


### Features

* add custom cache control middleware ([b76f886](https://github.com/TomaszKandula/TokanPages/commit/b76f88629a8097929029f69b776183e23d6ba77c))

# [0.25.0](https://github.com/TomaszKandula/TokanPages/compare/v0.24.0...v0.25.0) (2021-10-30)


### Bug Fixes

* add missing casting ([00332bb](https://github.com/TomaszKandula/TokanPages/commit/00332bb7fae79babe477181a726ae6c9a49041e4))
* add missing HTTP attribute ([754c1ee](https://github.com/TomaszKandula/TokanPages/commit/754c1eec4f3c7fabc1cc7fa1c537d308fba2d5d1))
* add missing key for div element in array ([e5e535c](https://github.com/TomaszKandula/TokanPages/commit/e5e535c44d378f3b3445bb5fa16fd3a6f697e02d))
* add missing key for div element in array ([4786a19](https://github.com/TomaszKandula/TokanPages/commit/4786a191d12af6435050de613880d5269cc53b4d))
* aligning default export name and file name ([a9f69f6](https://github.com/TomaszKandula/TokanPages/commit/a9f69f683368ae24dc530cd71fd42edb59abd3fc))
* change error code ([83b4409](https://github.com/TomaszKandula/TokanPages/commit/83b4409bd83ad3c91f17d9f00a51391b89033030))
* correct date and time implementation ([7eb29c2](https://github.com/TomaszKandula/TokanPages/commit/7eb29c280f84e5a8725a60c0aaecfbe8150d7ce0))
* correct implementation using base url, update tests and related code ([76b788f](https://github.com/TomaszKandula/TokanPages/commit/76b788f7cbb9a806511f76b0f674cc7b5fa5fe5e))
* refactor common code so it can be reused ([36dc56e](https://github.com/TomaszKandula/TokanPages/commit/36dc56ea741db6f074ef7d1c4bdd58e6d55a93d1))
* remove unnecessary semicolon ([f70ff57](https://github.com/TomaszKandula/TokanPages/commit/f70ff57edc403610880a960cf8aa43dc4d25c707))
* removed unnecessary React import ([c8c02fb](https://github.com/TomaszKandula/TokanPages/commit/c8c02fb9968d47c598f2d7e4f3690a9f7473c2d2))
* resolve merge issues ([2fb473d](https://github.com/TomaszKandula/TokanPages/commit/2fb473d7015515ce3377a1d3bf574437d678adee))
* use interfaces not implementations to create mock object ([7418f00](https://github.com/TomaszKandula/TokanPages/commit/7418f009adcc67a6938a1f7e6cad9d25072ce1fe))


### Features

* add command column to user tokens table ([f95a5d9](https://github.com/TomaszKandula/TokanPages/commit/f95a5d93167a611f926f0bfb8e199e6c05243789))
* add command name when saving new user token ([7654226](https://github.com/TomaszKandula/TokanPages/commit/7654226e471ea31ff4a9fddc481fc24f068cb233))
* add database context and logger to template handler ([b8d4703](https://github.com/TomaszKandula/TokanPages/commit/b8d47036368af2a4fe470b72b3d88f07b82d0091))
* add error code for empty email template ([644c9d0](https://github.com/TomaszKandula/TokanPages/commit/644c9d0f83cbf35e39cd391d2521e5f0022c1bed))
* add new error codes ([34f707b](https://github.com/TomaszKandula/TokanPages/commit/34f707b84259b49ba7ae4778689ef3b9dab764b9))
* add new property for current date time relative to UTC ([3809d97](https://github.com/TomaszKandula/TokanPages/commit/3809d97caf9ef0d4a2f8ec66e29f241f86f0b6fe))
* add table to store all article read counts ([0312db2](https://github.com/TomaszKandula/TokanPages/commit/0312db297545f1bb99c57258a856cc32d6989ea0))
* add user tokens to database ([34ea0b9](https://github.com/TomaszKandula/TokanPages/commit/34ea0b96d48d69adbe3f66f1b710601ebaff38f5))
* extend error codes by adding COMPONENT_NOT_FOUND ([3c5f266](https://github.com/TomaszKandula/TokanPages/commit/3c5f266de278d6e45d0c56499a5e1b486d0f136b))
* improve GetDataFromStorage implementation ([79d95e8](https://github.com/TomaszKandula/TokanPages/commit/79d95e84685a80e21a09a0f856150b69091421a2))
* improve implementation with new Azure Storage Blob SDK v12 ([6e81b53](https://github.com/TomaszKandula/TokanPages/commit/6e81b538db3b1d0fba7ddde6f2d5dcf40be0dd90))
* improve implementation, add exceptions ([e91b937](https://github.com/TomaszKandula/TokanPages/commit/e91b937208c8f1943f87e6a104ca6a565f6e8c87))
* improve SetCookie implementation ([ea0b3cf](https://github.com/TomaszKandula/TokanPages/commit/ea0b3cf972ca02cf6594f85c5bd9b927024c196d))
* save generated user token on (re)authentication to database ([b8ad533](https://github.com/TomaszKandula/TokanPages/commit/b8ad5337b3538f994215222bb2d97b3fc622772b))
* save individual read count for logged user for viewed article ([58af7ed](https://github.com/TomaszKandula/TokanPages/commit/58af7ed26aa332e2b137b149ad6843d01f73f53a))

# [0.24.0](https://github.com/TomaszKandula/TokanPages/compare/v0.23.0...v0.24.0) (2021-10-03)


### Features

* add article text to 'get article' response ([56ba933](https://github.com/TomaszKandula/TokanPages/commit/56ba933eb2038aedb2eca3f00c6ad5fa96174e0b))
* add custom http client ([7eafdc9](https://github.com/TomaszKandula/TokanPages/commit/7eafdc9e4f650b5192abc2f311deeb6fea85eccc))
* add GetContent functionality to retrieve data from Azure Storage (supports multi-language) ([2e6c886](https://github.com/TomaszKandula/TokanPages/commit/2e6c8862981d48a3a43945fca34398461fd59a0b))
* add JSON serializer/deserializer/token parser based on Newtonsoft implementation ([3d37f82](https://github.com/TomaszKandula/TokanPages/commit/3d37f82d5fc42a78a2d9a1812cdc457b8fca67c2))
* add new base controller for proxy controllers ([d1ad7d7](https://github.com/TomaszKandula/TokanPages/commit/d1ad7d77ee79f44d1374bd06b7b6bfb852227d89))
* add new methods to map object(s) from json token ([eb5f9c6](https://github.com/TomaszKandula/TokanPages/commit/eb5f9c64c5743579c365d32c5b296abbf65508eb))
* add new proxy controller for getting article main image ([600c4b8](https://github.com/TomaszKandula/TokanPages/commit/600c4b8c7a578249d6ccba5f51c40f0b9e586f98))
* add new proxy endpoint for getting assets from storage ([08282ae](https://github.com/TomaszKandula/TokanPages/commit/08282ae00c52fc08a5d711d782422c349d3f3770))

# [0.23.0](https://github.com/TomaszKandula/TokanPages/compare/v0.22.0...v0.23.0) (2021-09-26)


### Bug Fixes

* add missing roles, permissions and last logged ([aa9a511](https://github.com/TomaszKandula/TokanPages/commit/aa9a5110d73a8950f4eb1be0552efa0df56449c6))
* add missing type ([c84bf21](https://github.com/TomaszKandula/TokanPages/commit/c84bf2147a550134dfe05d3bd12ff942c2ff9a8f))
* add missing update DateTime ([7841114](https://github.com/TomaszKandula/TokanPages/commit/78411140dbec86776fb87c8efafbd6b458b508b8))
* add missing update last logged date time ([8bef435](https://github.com/TomaszKandula/TokanPages/commit/8bef43523aabef6062bb70cb8ade5436d2571180))
* correct import path ([b5d70f5](https://github.com/TomaszKandula/TokanPages/commit/b5d70f57ac143c7146042138584187c3d77dbc46))


### Features

* add action call to bring UserInfo window ([b8b6a01](https://github.com/TomaszKandula/TokanPages/commit/b8b6a015eaa7d769db93925dc4bf287a3bca9178))
* add new helper methods with tests ([6b13a15](https://github.com/TomaszKandula/TokanPages/commit/6b13a15e7e4f6c606cdefb63aa0994f1fc1d71ba))
* add user info dialog box ([4f78b7c](https://github.com/TomaszKandula/TokanPages/commit/4f78b7ca155d8f8755c894b56195a9501be0c1d6))
* add user roles and permissions capabilities for UI ([36978ff](https://github.com/TomaszKandula/TokanPages/commit/36978ff60257d4b7e125c40c24ce6736dba5091e))
* restore data after page refresh ([16219bd](https://github.com/TomaszKandula/TokanPages/commit/16219bd781f18f4ef5ee4792828dad76f9e22e17))

# [0.22.0](https://github.com/TomaszKandula/TokanPages/compare/v0.21.0...v0.22.0) (2021-09-25)


### Bug Fixes

* resolve logic mistake ([026f186](https://github.com/TomaszKandula/TokanPages/commit/026f186c1f5561dbd6e3a639dbb70c51ef3d9db2))


### Features

* add account activation capabilities ([b96bfcf](https://github.com/TomaszKandula/TokanPages/commit/b96bfcfbda06a1ee69c353250f8c0a448dcd5ca6))
* add activate user capability ([5ea5eb2](https://github.com/TomaszKandula/TokanPages/commit/5ea5eb2792e42557d9c8f83f661ee24a4dfd0fda))
* add activation feature to account registration ([7a920a7](https://github.com/TomaszKandula/TokanPages/commit/7a920a798d9b5cc7fd58b79ecffbe92331ac307c))
* add new email template ([ffc9283](https://github.com/TomaszKandula/TokanPages/commit/ffc92839de1962bef3c271ee960d9e1acf8bb2dd))
* add new error codes to resources ([7f25db9](https://github.com/TomaszKandula/TokanPages/commit/7f25db9fb7a4c660c87cee00d14cf105aea38ff4))
* add new property for activation feature ([a6eb364](https://github.com/TomaszKandula/TokanPages/commit/a6eb364e5aae2bea34b11e40fbc593e1eb284a26))
* add progress on scroll feature ([a53214a](https://github.com/TomaszKandula/TokanPages/commit/a53214aa2c076dd5a414ff5f60b39b7c73923ef7))
* add revoke refresh token capability ([1fe9448](https://github.com/TomaszKandula/TokanPages/commit/1fe944803a2dbefb09d84f6dff5a4533af354d81))

# [0.21.0](https://github.com/TomaszKandula/TokanPages/compare/v0.20.0...v0.21.0) (2021-09-16)


### Bug Fixes

* add missing password mask ([c0c2cf8](https://github.com/TomaszKandula/TokanPages/commit/c0c2cf88859068f096a3a8baefce4eeea60776d1))
* resolve dependencies issues within callbacks ([3678709](https://github.com/TomaszKandula/TokanPages/commit/3678709f61636edfae57c2b3bf5e775090041c77))
* resolve inappropriate condition ([330e5aa](https://github.com/TomaszKandula/TokanPages/commit/330e5aa26dd49fa44b782e50dce4996f3ce7740a))


### Features

* add expiration date ([e39e017](https://github.com/TomaszKandula/TokanPages/commit/e39e017d73194166e26f72227922cd1b1dfad0d3))
* add new error message ([b457674](https://github.com/TomaszKandula/TokanPages/commit/b45767432511825701f19280cc92a66849fa3659))

# [0.20.0](https://github.com/TomaszKandula/TokanPages/compare/v0.19.1...v0.20.0) (2021-09-13)


### Bug Fixes

* add missing action for resetting signed user status ([1f17ae1](https://github.com/TomaszKandula/TokanPages/commit/1f17ae124dfee40c63e07f697a90ab90762c8e7f))
* add missing reset id ([ea30110](https://github.com/TomaszKandula/TokanPages/commit/ea30110ea7254edb910860e19398f818df7fe318))
* add missing sign-up state reset ([8181cf7](https://github.com/TomaszKandula/TokanPages/commit/8181cf70758ec1a79db4cb8be564a5b231a5f9cb))
* add missing state reset ([8d5f6ff](https://github.com/TomaszKandula/TokanPages/commit/8d5f6ffa49d35a79adfc34a7a39322c26f9279ea))
* check prop value ([59693b7](https://github.com/TomaszKandula/TokanPages/commit/59693b7a9cba12eca396b896dbb6c2ac81536d1d))
* correct component name ([ae89ad7](https://github.com/TomaszKandula/TokanPages/commit/ae89ad715af64912e271b1d07f450e6657c12781))
* resolve incorrect condition issue ([0dadcc5](https://github.com/TomaszKandula/TokanPages/commit/0dadcc577b3e3ff4ce6c2b4c2aa97d887d3cb58e))
* resolve issues with tests ([0a0c6c7](https://github.com/TomaszKandula/TokanPages/commit/0a0c6c77fdcc8cfc5c4a6e2fa6aa1e70f8acc8c7))
* resolve required roles issue ([0f0a315](https://github.com/TomaszKandula/TokanPages/commit/0f0a3159b2a2b36b5c28f16bd696ff29b62e5875))
* update dependencies ([9445fc0](https://github.com/TomaszKandula/TokanPages/commit/9445fc0c56eedc72997828b86d229c6652424ef5))


### Features

* add account activation check ([fcf96fa](https://github.com/TomaszKandula/TokanPages/commit/fcf96fac425ac2a39e620b317635867608006b8b))
* add action 'API call' to execute user signin ([5e8f688](https://github.com/TomaszKandula/TokanPages/commit/5e8f6881e59e6bf69a56102f05dd82f47b0531b7))
* add API call for user sing-up ([d8d1b7f](https://github.com/TomaszKandula/TokanPages/commit/d8d1b7fe529564f42226b3a19cc1df0f727d7782))
* add constants for Signin, Signup and Reset forms ([81aef3d](https://github.com/TomaszKandula/TokanPages/commit/81aef3d270d9189fd3697774a8ce0998aca8bd5e))
* add models for login functionality ([0187eec](https://github.com/TomaszKandula/TokanPages/commit/0187eecc522e771252b7741cf48b5b42929b13c3))
* add new action for update/reset password ([783a346](https://github.com/TomaszKandula/TokanPages/commit/783a346cd7729856d46c2334c32488da44bc9277))
* add new API urls for authentication functionality ([efdfdaf](https://github.com/TomaszKandula/TokanPages/commit/efdfdaf0ef6b0e5cf6824a2c8e7432053e21f0c1))
* add new constants for ([6b10e9c](https://github.com/TomaszKandula/TokanPages/commit/6b10e9c3718fc0acb21ae340bcd8c2ae6c7711a7))
* add new controller for updating user password ([5658fa4](https://github.com/TomaszKandula/TokanPages/commit/5658fa4e0a424aeb1d75e6bb780f81377a742a57))
* add new DTOs for update/reset password actions ([55e10c1](https://github.com/TomaszKandula/TokanPages/commit/55e10c19553df243ac48e928e32ffadde237af42))
* add new endpoints and message for update/reset password action ([b3e6c0b](https://github.com/TomaszKandula/TokanPages/commit/b3e6c0b6015ca66a83defe0566bf348376d757c6))
* add new error code ([5f8d41e](https://github.com/TomaszKandula/TokanPages/commit/5f8d41e27ca27eccca09247818c6cc1c0b044d23))
* add new exports for login functionality ([6359f3d](https://github.com/TomaszKandula/TokanPages/commit/6359f3dc6b4d1651a832af0d881acb9bf12b774a))
* add new validation rule ([c9d8cf7](https://github.com/TomaszKandula/TokanPages/commit/c9d8cf772a85e6fe9aebf1bfbe2a0eae530edef5))
* add new validation rules for Signin functionality ([724ce59](https://github.com/TomaszKandula/TokanPages/commit/724ce598abf210e778cbbbecbd22d8c9cbaa07fc))
* add new validation rules for update password form ([e562ca9](https://github.com/TomaszKandula/TokanPages/commit/e562ca94dde6d7bf5af99759dfffa9afa8b3cd0f))
* add redux action and reducer for login functionality (incl. state, default values) ([410de99](https://github.com/TomaszKandula/TokanPages/commit/410de99f31b103bc1755d8f14c3b10817c19fa39))
* add reset password handler and controller ([9125041](https://github.com/TomaszKandula/TokanPages/commit/9125041d373c768d9550cbb40910a6192360a81b))
* add reset password UI ([b2a9c59](https://github.com/TomaszKandula/TokanPages/commit/b2a9c591353b7c45fc09f0feceea827ed3a39498))
* add separate contact page ([210daa8](https://github.com/TomaszKandula/TokanPages/commit/210daa809638245cdfdebc4630cbe902441c574e))
* add signin basic logic and keep it in separate file (split view from logic) ([f022e86](https://github.com/TomaszKandula/TokanPages/commit/f022e8623b68e675aaeceb47af896d6c5f34ac4f))
* add signup implementation ([4ae9e5a](https://github.com/TomaszKandula/TokanPages/commit/4ae9e5a7e018ab0c09d81b556f6fc7392f9bfeb1))
* add update password functionality ([f83a960](https://github.com/TomaszKandula/TokanPages/commit/f83a960dca2c5c7693a70c6796a33640d8d8d764))
* implement command, handler and validator for updating user password functionality ([0057445](https://github.com/TomaszKandula/TokanPages/commit/0057445f0ea0f69f776de1c37d035b7c1178dd58))

## [0.19.1](https://github.com/TomaszKandula/TokanPages/compare/v0.19.0...v0.19.1) (2021-07-17)


### Bug Fixes

* add missing Secure property for CookieOptions ([baadf2b](https://github.com/TomaszKandula/TokanPages/commit/baadf2b10ac61f88a9ca5c22d86328a4b046ff11))

# [0.19.0](https://github.com/TomaszKandula/TokanPages/compare/v0.18.0...v0.19.0) (2021-07-17)


### Features

* add endpoint for re-authentication ([d337a00](https://github.com/TomaszKandula/TokanPages/commit/d337a00c294bd959d8fedbc1093fdc24f5eee2dd))
* add new model for RefreshToken ([898bdde](https://github.com/TomaszKandula/TokanPages/commit/898bddee7666d485683b4748df4b655fea9c4940))
* add re-authenticate command, handler and validator ([2aac651](https://github.com/TomaszKandula/TokanPages/commit/2aac6518d64d7578b1d8bd943512aec755bfe9a1))
* add RefreshToken and JWT generator in same class ([dbe13ff](https://github.com/TomaszKandula/TokanPages/commit/dbe13ff717452ae2061c545e313a2c5c54e6b1f4))

# [0.18.0](https://github.com/TomaszKandula/TokanPages/compare/v0.17.0...v0.18.0) (2021-07-05)


### Bug Fixes

* add missing attribute for update endpoint ([8dc38cf](https://github.com/TomaszKandula/TokanPages/commit/8dc38cf61b1041c8bf6c600a00e6a487f237b2b2))
* add missing try_files to handle routing ([9457de2](https://github.com/TomaszKandula/TokanPages/commit/9457de233ea124de6d37d505aba0179689792c42))


### Features

* add authorization policies, claims and roles ([87fc0be](https://github.com/TomaszKandula/TokanPages/commit/87fc0be89294720615f3f2d4d50eec657302d366))
* add Identity service ([e68230b](https://github.com/TomaszKandula/TokanPages/commit/e68230bdf9803a347f038b4ba1e63a8455f3f1a7))
* add JWT bearer configuration ([dffb9c5](https://github.com/TomaszKandula/TokanPages/commit/dffb9c584b5f947d19970b04b92c5667b36f8e2b))
* add new BCrypt implementation ([ab6dae9](https://github.com/TomaszKandula/TokanPages/commit/ab6dae9c7c97d9a9771cb5dbe1ef1c819546bb82))

# [0.17.0](https://github.com/TomaszKandula/TokanPages/compare/v0.16.1...v0.17.0) (2021-06-23)


### Features

* add health check controller ([d098f2c](https://github.com/TomaszKandula/TokanPages/commit/d098f2c80a3f101f427151e57587c9e0f81c97fd))
* add new error code for smtp client ([3942b22](https://github.com/TomaszKandula/TokanPages/commit/3942b22d0f1879e38f0ff7a94c6f22b8bef51da9))
* add new inner massage property ([322062a](https://github.com/TomaszKandula/TokanPages/commit/322062aecd4c961ba5928258059090e9d818dd19))
* add new method for connection check, use cancellation token ([4fe8bb1](https://github.com/TomaszKandula/TokanPages/commit/4fe8bb191367b19e037267683e16ae5d51116e0a))
* add new test for health check endpoint ([25c7a3d](https://github.com/TomaszKandula/TokanPages/commit/25c7a3de8bff41ad1f7c06cc9aed47bec63022e6))

## [0.16.1](https://github.com/TomaszKandula/TokanPages/compare/v0.16.0...v0.16.1) (2021-06-17)


### Bug Fixes

* check input value, and missing types ([84f14ae](https://github.com/TomaszKandula/TokanPages/commit/84f14ae0ce666b752511e20846e6ccc9cc698c03))

# [0.16.0](https://github.com/TomaszKandula/TokanPages/compare/v0.15.1...v0.16.0) (2021-06-15)


### Features

* add list of metric names ([68fbc0b](https://github.com/TomaszKandula/TokanPages/commit/68fbc0bfe811a92c254baab1adc7173ec0e0a4b3))
* add SonarQube metric name verification ([11d8c1d](https://github.com/TomaszKandula/TokanPages/commit/11d8c1da08b6c24f6e9ec3e33760eba4869f2283))

## [0.15.1](https://github.com/TomaszKandula/TokanPages/compare/v0.15.0...v0.15.1) (2021-06-15)


### Bug Fixes

* correct docker secrets for the frontend job ([759be1e](https://github.com/TomaszKandula/TokanPages/commit/759be1e6f13d995d5793cc368c7156252a49419e))

# [0.15.0](https://github.com/TomaszKandula/TokanPages/compare/v0.14.1...v0.15.0) (2021-06-15)


### Features

* add SonarQube to client-app ([88e69af](https://github.com/TomaszKandula/TokanPages/commit/88e69af152d611796c5bf1d41086f92348944c13))

## [0.14.1](https://github.com/TomaszKandula/TokanPages/compare/v0.14.0...v0.14.1) (2021-06-01)


### Bug Fixes

* remove Newtonsoft.Json from code and use System.Text.Json ([b99f359](https://github.com/TomaszKandula/TokanPages/commit/b99f35984a24bee3b29544bb31eacfdbfb456311))

# [0.14.0](https://github.com/TomaszKandula/TokanPages/compare/v0.13.1...v0.14.0) (2021-06-01)


### Features

* extend application error by 'inner message' property ([4093307](https://github.com/TomaszKandula/TokanPages/commit/40933076bfe963e53eecb851aa0fa315bccde1cf))
* implement improved exception handler ([88dd7d9](https://github.com/TomaszKandula/TokanPages/commit/88dd7d9aa3c303c60219e45d7ed91dea74e916a9))

## [0.13.1](https://github.com/TomaszKandula/TokanPages/compare/v0.13.0...v0.13.1) (2021-06-01)


### Bug Fixes

* change configuration to avoid incorrect status code issues ([528fdf6](https://github.com/TomaszKandula/TokanPages/commit/528fdf6d8197148b0b39d3fe660221705040b37f))

# [0.13.0](https://github.com/TomaszKandula/TokanPages/compare/v0.12.0...v0.13.0) (2021-05-31)


### Features

* add dialog box message helpers ([6fc38c8](https://github.com/TomaszKandula/TokanPages/commit/6fc38c84314f00d047c5b25d6ddcea519b02e6ed))
* add new action for DialogBox ([42b7f24](https://github.com/TomaszKandula/TokanPages/commit/42b7f2467b85aa388548f2eee1f4e8844be9c077))
* add new application dialog box ([c914c15](https://github.com/TomaszKandula/TokanPages/commit/c914c15c67b6d52efa032560ab6059d41ca08a09))

# [0.12.0](https://github.com/TomaszKandula/TokanPages/compare/v0.11.0...v0.12.0) (2021-05-30)


### Bug Fixes

* add bad request for missing parameters ([5187f9f](https://github.com/TomaszKandula/TokanPages/commit/5187f9fd81bdbc60b72c5269b49734d3e4f5943c))
* add missing base ([9cee86c](https://github.com/TomaszKandula/TokanPages/commit/9cee86cdc9bd288c9712f6fdea3f941f0ef67f8e))
* add missing Serializable attribute ([fc058d6](https://github.com/TomaszKandula/TokanPages/commit/fc058d6fe6291c6b1311409322de6da6079f0950))
* add protected constructors due to ISerializable ([82ece1e](https://github.com/TomaszKandula/TokanPages/commit/82ece1e1a82d76ab399bd5614525c8003aa69426))
* correct file name ([5350a73](https://github.com/TomaszKandula/TokanPages/commit/5350a7373860ed419a61606bffe012d808f107b9))
* correct folder name ([19da276](https://github.com/TomaszKandula/TokanPages/commit/19da276d0e5146fb5259d9a216051438a4a1c36d))
* make method static ([67d3eeb](https://github.com/TomaszKandula/TokanPages/commit/67d3eeb64f825fc01d0dea604d0bb695ada5b95c))


### Features

* add endpoint for SonarQube quality gate large badge ([a1e516c](https://github.com/TomaszKandula/TokanPages/commit/a1e516c9785e41e5ff421d65859de7450307b985))
* add ETag attribute filter ([c92dff3](https://github.com/TomaszKandula/TokanPages/commit/c92dff3090a04dff93957630d9e4407b047f9452))

# [0.11.0](https://github.com/TomaszKandula/TokanPages/compare/v0.10.2...v0.11.0) (2021-05-29)


### Features

* add MIME type ([4bd80dc](https://github.com/TomaszKandula/TokanPages/commit/4bd80dc06b1fa331d6f2aa2ac30f2cc1dd97ba22))
* add new controller for metrics from SonarQube project ([fda2283](https://github.com/TomaszKandula/TokanPages/commit/fda2283c1e8a2d8f38ff3fc1d57de438fd0a42b3))
* add SonarQube metrics ([62279c6](https://github.com/TomaszKandula/TokanPages/commit/62279c69494c2a12d289009e834f0b6ec0ac69fe))

## [0.10.2](https://github.com/TomaszKandula/TokanPages/compare/v0.10.1...v0.10.2) (2021-05-28)


### Bug Fixes

* add missing cancellation token ([0bd6ed0](https://github.com/TomaszKandula/TokanPages/commit/0bd6ed02803fb8aef442fc2db3c8b0f86b150d7a))

## [0.10.1](https://github.com/TomaszKandula/TokanPages/compare/v0.10.0...v0.10.1) (2021-05-26)


### Bug Fixes

* add missing error dispatch for GetData method ([781518b](https://github.com/TomaszKandula/TokanPages/commit/781518b47364607295aa548d2e6efee20b586c9a))
* resolve performance issues with actions ([eeae8bc](https://github.com/TomaszKandula/TokanPages/commit/eeae8bc8817df7d76c239f572463e777545f2807))

# [0.10.0](https://github.com/TomaszKandula/TokanPages/compare/v0.9.1...v0.10.0) (2021-05-26)


### Bug Fixes

* correct dependencies ([8d92d25](https://github.com/TomaszKandula/TokanPages/commit/8d92d2577e0a0545fe0cbdebbab2f7308fcf9c47))
* resolve 'undefined' issues ([a4aeb57](https://github.com/TomaszKandula/TokanPages/commit/a4aeb57574c687040bdd1cf593a7889a980336f5))


### Features

* expand Redux, add actions/reducers/states/defaults ([e85b7e3](https://github.com/TomaszKandula/TokanPages/commit/e85b7e37a66da71ccade313760489d704177bb23))

## [0.9.1](https://github.com/TomaszKandula/TokanPages/compare/v0.9.0...v0.9.1) (2021-05-23)


### Bug Fixes

* add missing 'build-arg' ([0cd597f](https://github.com/TomaszKandula/TokanPages/commit/0cd597ff02aeadbe376418d25ec581208341adbd))

# [0.9.0](https://github.com/TomaszKandula/TokanPages/compare/v0.8.0...v0.9.0) (2021-05-23)


### Features

* setup release date/time in workflow job ([82b735c](https://github.com/TomaszKandula/TokanPages/commit/82b735ce20902fb4a8fa8d7414878c8ef8f4049b))

# [0.8.0](https://github.com/TomaszKandula/TokanPages/compare/v0.7.3...v0.8.0) (2021-05-23)


### Features

* split version number, and date/time ([bf1b2bf](https://github.com/TomaszKandula/TokanPages/commit/bf1b2bf731c31db65998f45d8e059e2c27577df1))

## [0.7.3](https://github.com/TomaszKandula/TokanPages/compare/v0.7.2...v0.7.3) (2021-05-23)


### Bug Fixes

* remove {VERSION} issue from the page footer ([1856bbd](https://github.com/TomaszKandula/TokanPages/commit/1856bbd7d23488563e36bba279be401fac16e5cc))

## [0.7.2](https://github.com/TomaszKandula/TokanPages/compare/v0.7.1...v0.7.2) (2021-05-23)


### Bug Fixes

* use one RUN command with multiple lines ([f9c7d07](https://github.com/TomaszKandula/TokanPages/commit/f9c7d07f3c50884944ab3de8253447ff6c3e7055))

## [0.7.1](https://github.com/TomaszKandula/TokanPages/compare/v0.7.0...v0.7.1) (2021-05-23)


### Bug Fixes

* correct release date time ([9027123](https://github.com/TomaszKandula/TokanPages/commit/90271237671f6e7fffc9f4fae719febc3c915641))

# [0.7.0](https://github.com/TomaszKandula/TokanPages/compare/v0.6.0...v0.7.0) (2021-05-23)


### Bug Fixes

* correct page footer display ([52eac1d](https://github.com/TomaszKandula/TokanPages/commit/52eac1d51a3dcfd964472dad946cb24229dcc6b9))


### Features

* add http client retry policy with Polly ([a032a83](https://github.com/TomaszKandula/TokanPages/commit/a032a83b76642c2ed97586f27392c9d5645df620))
* add Polly to the main project ([c0b1f1d](https://github.com/TomaszKandula/TokanPages/commit/c0b1f1d1a5e539eb8931bc4b250e6b97f58264d6))

# [0.6.0](https://github.com/TomaszKandula/TokanPages/compare/v0.5.9...v0.6.0) (2021-05-15)


### Bug Fixes

* add missing node and yarn ([aac899e](https://github.com/TomaszKandula/TokanPages/commit/aac899e3f915576788bbb3f12ddcb51f78fc9325))
* add missing UserId from UserProvider ([f2f08f7](https://github.com/TomaszKandula/TokanPages/commit/f2f08f778bc2aa7e7fa3dae98fc4f5edba7488a8))
* allow anonymous user to only add likes and inc. read count ([abd212a](https://github.com/TomaszKandula/TokanPages/commit/abd212a84a8cbd85607f3577eb91920a9668ac43))
* change first letter of file name ([f31e1dc](https://github.com/TomaszKandula/TokanPages/commit/f31e1dcc8c8b6ebcaae36345a0ce53540d453700))
* check domain if supplied address is correct ([9a2154f](https://github.com/TomaszKandula/TokanPages/commit/9a2154f9fa79f7b0c7ead50c3698882f5094dfbb))
* correct type ([e3c2034](https://github.com/TomaszKandula/TokanPages/commit/e3c2034ddf0feadf0bbb0ca460973f9aa0fca65d))
* correct type in variable name ([9294ccd](https://github.com/TomaszKandula/TokanPages/commit/9294ccdb98d7f6484741c10a778e8808a3f9c563))
* correct typo in namespaces and folder name ([27ee879](https://github.com/TomaszKandula/TokanPages/commit/27ee879b65503896d8fe70583abed08c0c127ee7))
* disable changing content to its default state, just hide window ([dcdc416](https://github.com/TomaszKandula/TokanPages/commit/dcdc41624731d0336a04fdeb85f2bf25b399e42a))
* only logged user can add article, add ACCESS_DENIED exception ([f7ac20b](https://github.com/TomaszKandula/TokanPages/commit/f7ac20bd3c08cc159aa4d85a67301f64d2bf2880))
* put test projects in same root folder to fix ContentRootPath issue with WebApplicationFactory ([bab01d4](https://github.com/TomaszKandula/TokanPages/commit/bab01d453c0e02174f27d7ba1ed234a5d856ff02))
* remove typo from method name ([1be461e](https://github.com/TomaszKandula/TokanPages/commit/1be461edadcd34c883390ec73c7d6f74ce45d8b1))
* replace TAB with SPACE (4 indents) ([96a28ca](https://github.com/TomaszKandula/TokanPages/commit/96a28cafee159a41ace83cd6061c95c0a25e17a5))
* resolving issue with adding new subscriber ([f07cf01](https://github.com/TomaszKandula/TokanPages/commit/f07cf01be72fef759574166c5203d3da0e5123be))
* typo ([9e898be](https://github.com/TomaszKandula/TokanPages/commit/9e898be2ab9101845c72f4fe50cdf3848f5e84f0))
* update imports ([8dab1ea](https://github.com/TomaszKandula/TokanPages/commit/8dab1eaebf205bac6daf48250e737061d3f7d211))


### Features

* add 'IRaiseError' to app. state ([6685f13](https://github.com/TomaszKandula/TokanPages/commit/6685f13ee4547457880692ad0e08f260f08c8c9e))
* add action, reducer, state and default values for error action ([06935a4](https://github.com/TomaszKandula/TokanPages/commit/06935a497fae5fbadaad4f5b0e656be7f2b8d111))
* add app toast for errors ([#421](https://github.com/TomaszKandula/TokanPages/issues/421)) ([05d98de](https://github.com/TomaszKandula/TokanPages/commit/05d98de65f8d49d45b1c2e388479572af4f62924))
* add code analysis exclusion ([7d7380c](https://github.com/TomaszKandula/TokanPages/commit/7d7380c58690446a871ae6421190a052e777e304))
* add enum extension ([5237f4b](https://github.com/TomaszKandula/TokanPages/commit/5237f4b72a8a43b2513e2c15464fc62d352dfd80))
* add error handling ([ff112b5](https://github.com/TomaszKandula/TokanPages/commit/ff112b560042e32a888fde94df0c392d0c4a474c))
* add error handling to redux ([f0b00e5](https://github.com/TomaszKandula/TokanPages/commit/f0b00e58b7e54b418a5123be3a1ebafd4bc427cd))
* add error message extractor ([#425](https://github.com/TomaszKandula/TokanPages/issues/425)) ([3779f15](https://github.com/TomaszKandula/TokanPages/commit/3779f15656e8e687667cf866e263f80737e80ae5))
* add logging configuration ([6b66f6d](https://github.com/TomaszKandula/TokanPages/commit/6b66f6dc155ca5a603ca14d60ffbfc3a99d16b97))
* add new azure blob storage impl. ([#433](https://github.com/TomaszKandula/TokanPages/issues/433)) ([2d55fad](https://github.com/TomaszKandula/TokanPages/commit/2d55fad562085594ebc57cab137d89f87161ce23))
* add new database tables ([#403](https://github.com/TomaszKandula/TokanPages/issues/403)) ([7d9ae85](https://github.com/TomaszKandula/TokanPages/commit/7d9ae85800a003a7b97b414d2e46e6e22ce2076c))
* add new DateTime service to Core ([1ac216c](https://github.com/TomaszKandula/TokanPages/commit/1ac216c89794c491c8160cde6ec68f79e0906440))
* add new db tables (for photo album feature) ([a513cb7](https://github.com/TomaszKandula/TokanPages/commit/a513cb776624d2fd20465e36a78b7f71172cd261))
* add new enums ([443d481](https://github.com/TomaszKandula/TokanPages/commit/443d481e0b9dbb384e4f628e1da820e9505b4c26))
* add new error code ([940a7d6](https://github.com/TomaszKandula/TokanPages/commit/940a7d60272f3cba52b10f35292e27be8dd54ebe))
* add new migrations ([0ccb4b7](https://github.com/TomaszKandula/TokanPages/commit/0ccb4b76abc78b218d5a138bc508f12b8a095e11))
* add new SVG icon ([#448](https://github.com/TomaszKandula/TokanPages/issues/448)) ([a9b0719](https://github.com/TomaszKandula/TokanPages/commit/a9b0719f3a9af29b4826078d6bf4fb8d75317417))
* add new validator helpers ([effc6b9](https://github.com/TomaszKandula/TokanPages/commit/effc6b937abec9a32ca75753e4310d48be092174))
* add package.json with semantic-release to project folder ([70d35ae](https://github.com/TomaszKandula/TokanPages/commit/70d35ae1d4ea78b1781861a27c61300396397473))
* add props for paging feature functionality ([d3ad033](https://github.com/TomaszKandula/TokanPages/commit/d3ad03386a9286d90c834917189080ca14d6b84f))
* add reducer and action implementation ([aa05298](https://github.com/TomaszKandula/TokanPages/commit/aa052987df4dabb31ec0d54289988d6ab5c5fe48))
* add sentry exception ([33dcf8c](https://github.com/TomaszKandula/TokanPages/commit/33dcf8c7d1f021406333c7c2c63a4e77282cb275))
* blob storage ([#437](https://github.com/TomaszKandula/TokanPages/issues/437)) ([66a25c2](https://github.com/TomaszKandula/TokanPages/commit/66a25c20e6c2f80041ab5f589495ea8b4882270e))
* make image clickable if URL is given (redirect to) ([e5d8f46](https://github.com/TomaszKandula/TokanPages/commit/e5d8f466412c1a8c7be11b534b4f510d5a2b9d8e))
* new database setup for development ([#445](https://github.com/TomaszKandula/TokanPages/issues/445)) ([74b28d8](https://github.com/TomaszKandula/TokanPages/commit/74b28d823f5c495081ddbe32a037be4084ab9439))
* new storage implementation ([#439](https://github.com/TomaszKandula/TokanPages/issues/439)) ([0deffff](https://github.com/TomaszKandula/TokanPages/commit/0defffff21e060494f4b4821cf6b4f446eaeeb07))
* redirect to 500px service ([2592631](https://github.com/TomaszKandula/TokanPages/commit/2592631ae28985d802950b1cf60691f10a168f66))
* setup Sentry for ReactJs to monitor ClientApp ([e32aa53](https://github.com/TomaszKandula/TokanPages/commit/e32aa5318b651969c7792119269ea158d693ea5d))
* use diff. components for title, subtitle and header ([#405](https://github.com/TomaszKandula/TokanPages/issues/405)) ([1dc78f2](https://github.com/TomaszKandula/TokanPages/commit/1dc78f2915d62072cb0c6012b5e2b62c8648da0c))
