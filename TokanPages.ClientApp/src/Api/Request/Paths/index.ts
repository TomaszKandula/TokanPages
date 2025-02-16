const API_VER = process.env.REACT_APP_API_VER;
const APP_BACKEND = process.env.REACT_APP_BACKEND;
export const API_BASE_URI = `${APP_BACKEND}/api/v${API_VER}`;
const API_ARTICLES_URI = `${API_BASE_URI}/articles`;
const API_USERS_URI = `${API_BASE_URI}/users`;
const API_NOTIFICATIONS_WEB_URI = `${API_BASE_URI}/notifications/web`;
const API_NEWSLETTERS_URI = `${API_BASE_URI}/sender/newsletters`;
const API_MAILER_URI = `${API_BASE_URI}/sender/mailer`;
const API_CONTENT_URI = `${API_BASE_URI}/content/components`;
const API_ASSETS_URI = `${API_BASE_URI}/content/assets`;
const API_NON_VIDEO_ASSETS_URI = `${API_ASSETS_URI}/getNonVideoAsset`;

export const GET_ARTICLE_INFO = `${API_ARTICLES_URI}/{id}/getArticleInfo`;
export const GET_ARTICLES = `${API_ARTICLES_URI}/getArticles`;
export const GET_ARTICLE = `${API_ARTICLES_URI}/{id}/getArticle`;
export const GET_ARTICLE_BY_TITLE = `${API_ARTICLES_URI}/{title}/getArticle`;
export const ADD_ARTICLE = `${API_ARTICLES_URI}/addArticle`;
export const UPDATE_ARTICLE_CONTENT = `${API_ARTICLES_URI}/updateArticleContent`;
export const UPDATE_ARTICLE_COUNT = `${API_ARTICLES_URI}/updateArticleCount`;
export const UPDATE_ARTICLE_LIKES = `${API_ARTICLES_URI}/updateArticleLikes`;
export const UPDATE_ARTICLE_VISIBILITY = `${API_ARTICLES_URI}/updateArticleVisibility`;
export const REMOVE_ARTICLE = `${API_ARTICLES_URI}/removeArticle`;

export const ACTIVATE_USER = `${API_USERS_URI}/activateUser`;
export const AUTHENTICATE = `${API_USERS_URI}/authenticateUser`;
export const REAUTHENTICATE = `${API_USERS_URI}/reAuthenticateUser`;
export const VERIFY_USER_EMAIL = `${API_USERS_URI}/requestEmailVerification`;
export const REVOKE_USER_TOKEN = `${API_USERS_URI}/revokeUserToken`;
export const REVOKE_REFRESH_TOKEN = `${API_USERS_URI}/revokeUserRefreshToken`;
export const GET_USER = `${API_USERS_URI}/{id}/getUser`;
export const GET_USERS = `${API_USERS_URI}/getUsers`;
export const GET_USER_IMAGE = `${API_USERS_URI}/{id}/getUserImage?blobName={name}`;
export const GET_USER_VIDEO = `${API_USERS_URI}/{id}/getUserVideo?blobName={name}`;
export const ADD_USER = `${API_USERS_URI}/addUser`;
export const UPDATE_USER = `${API_USERS_URI}/updateUser`;
export const REMOVE_USER = `${API_USERS_URI}/removeUser`;
export const RESET_USER_PASSWORD = `${API_USERS_URI}/resetUserPassword`;
export const UPDATE_USER_PASSWORD = `${API_USERS_URI}/updateUserPassword`;
export const UPLOAD_USER_IMAGE = `${API_USERS_URI}/uploadImage`;
export const UPLOAD_USER_VIDEO = `${API_USERS_URI}/uploadVideo`;
export const GET_USER_NOTE = `${API_USERS_URI}/{id}/getUserNote`;
export const GET_USER_NOTES = `${API_USERS_URI}/getUserNotes`;
export const ADD_USER_NOTE = `${API_USERS_URI}/addUserNote`;
export const UPDATE_USER_NOTE  = `${API_USERS_URI}/updateUserNote`;
export const REMOVE_USER_NOTE = `${API_USERS_URI}/removeUserNote`;

export const GET_NEWSLETTERS = `${API_NEWSLETTERS_URI}/getNewsletters`;
export const GET_NEWSLETTER = `${API_NEWSLETTERS_URI}/{id}/getNewsletter`;
export const ADD_NEWSLETTER = `${API_NEWSLETTERS_URI}/addNewsletter`;
export const UPDATE_NEWSLETTER = `${API_NEWSLETTERS_URI}/updateNewsletter`;
export const REMOVE_NEWSLETTER = `${API_NEWSLETTERS_URI}/removeNewsletter`;

export const SEND_MESSAGE = `${API_MAILER_URI}/sendMessage`;
export const SEND_NEWSLETTER = `${API_MAILER_URI}/sendNewsletter`;

export const NOTIFY_WEB_URL = `${API_NOTIFICATIONS_WEB_URI}/notify`;
export const NOTIFICATION_STATUS = `${API_NOTIFICATIONS_WEB_URI}/status`;

export const GET_NON_VIDEO_ASSET = `${API_ASSETS_URI}/getNonVideoAsset?blobName={name}`;
export const GET_VIDEO_ASSET = `${API_ASSETS_URI}/getVideoAsset?blobName={name}`;

export const GET_CONTENT_MANIFEST = `${API_CONTENT_URI}/getManifest`;
export const REQUEST_PAGE_DATA = `${API_CONTENT_URI}/requestPageData`;

export const GET_ARTICLE_MAIN_IMAGE_URL = `${API_ASSETS_URI}/getArticleAsset?id={id}&assetName=image.webp`;

export const GET_DOCUMENTS_URL = `${API_NON_VIDEO_ASSETS_URI}?blobName=documents`;
export const GET_IMAGES_URL = `${API_NON_VIDEO_ASSETS_URI}?blobName=images`;
export const GET_ARTICLE_IMAGE_URL = `${API_NON_VIDEO_ASSETS_URI}?blobName=images/sections/articles`;
export const GET_FEATURED_IMAGE_URL = `${API_NON_VIDEO_ASSETS_URI}?blobName=images/sections/featured`;
export const GET_TESTIMONIALS_URL = `${API_NON_VIDEO_ASSETS_URI}?blobName=images/sections/testimonials`;
export const GET_SOCIALS_URL = `${API_NON_VIDEO_ASSETS_URI}?blobName=images/sections/socials`;
export const GET_ICONS_URL = `${API_NON_VIDEO_ASSETS_URI}?blobName=images/icons`;
export const GET_FLAG_URL = `${API_NON_VIDEO_ASSETS_URI}?blobName=images/flags`;

export const ARTICLE_PATH = "/articles?title={title}";
