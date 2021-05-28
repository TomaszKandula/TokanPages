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
