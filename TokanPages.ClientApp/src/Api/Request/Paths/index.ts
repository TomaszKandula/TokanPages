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

export const GET_ARTICLES = `${API_ARTICLES_URI}/getArticles`;
export const GET_ARTICLE = `${API_ARTICLES_URI}/{id}/getArticle`;
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
export const GET_USER_IMAGE = `${API_USERS_URI}/{id}/getUserImage/?blobName={name}`;
export const GET_USER_VIDEO = `${API_USERS_URI}/{id}/getUserVideo/?blobName={name}`;
export const ADD_USER = `${API_USERS_URI}/addUser`;
export const UPDATE_USER = `${API_USERS_URI}/updateUser`;
export const REMOVE_USER = `${API_USERS_URI}/removeUser`;
export const RESET_USER_PASSWORD = `${API_USERS_URI}/resetUserPassword`;
export const UPDATE_USER_PASSWORD = `${API_USERS_URI}/updateUserPassword`;
export const UPLOAD_USER_IMAGE = `${API_USERS_URI}/uploadImage`;
export const UPLOAD_USER_VIDEO = `${API_USERS_URI}/uploadVideo`;

export const GET_NEWSLETTERS = `${API_NEWSLETTERS_URI}/getNewsletters`;
export const GET_NEWSLETTER = `${API_NEWSLETTERS_URI}/{id}/getNewsletter`;
export const ADD_NEWSLETTER = `${API_NEWSLETTERS_URI}/addNewsletter`;
export const UPDATE_NEWSLETTER = `${API_NEWSLETTERS_URI}/updateNewsletter`;
export const REMOVE_NEWSLETTER = `${API_NEWSLETTERS_URI}/removeNewsletter`;

export const SEND_MESSAGE = `${API_MAILER_URI}/sendMessage`;
export const SEND_NEWSLETTER = `${API_MAILER_URI}/sendNewsletter`;

export const NOTIFY_WEB_URL = `${API_NOTIFICATIONS_WEB_URI}/notify`;
export const NOTIFICATION_STATUS = `${API_NOTIFICATIONS_WEB_URI}/status`;

export const GET_NON_VIDEO_ASSET = `${API_ASSETS_URI}/getNonVideoAsset/?blobName={name}`;
export const GET_VIDEO_ASSET = `${API_ASSETS_URI}/getVideoAsset/?blobName={name}`;

export const GET_CONTENT_MANIFEST = `${API_CONTENT_URI}/getManifest`;
export const GET_CONTENT_TEMPLATES = `${API_CONTENT_URI}/getContent/?name=templates&type=component`;
export const GET_NAVIGATION_CONTENT = `${API_CONTENT_URI}/getContent/?name=navigation&type=component`;
export const GET_HEADER_CONTENT = `${API_CONTENT_URI}/getContent/?name=header&type=component`;
export const GET_FOOTER_CONTENT = `${API_CONTENT_URI}/getContent/?name=footer&type=component`;
export const GET_ARTICLE_FEAT_CONTENT = `${API_CONTENT_URI}/getContent/?name=articleFeatures&type=component`;
export const GET_CONTACT_FORM_CONTENT = `${API_CONTENT_URI}/getContent/?name=contactForm&type=component`;
export const GET_COOKIES_PROMPT_CONTENT = `${API_CONTENT_URI}/getContent/?name=cookiesPrompt&type=component`;
export const GET_CLIENTS_CONTENT = `${API_CONTENT_URI}/getContent/?name=clients&type=component`;
export const GET_FEATURED_CONTENT = `${API_CONTENT_URI}/getContent/?name=featured&type=component`;
export const GET_TECHNOLOGIES_CONTENT = `${API_CONTENT_URI}/getContent/?name=technologies&type=component`;
export const GET_NEWSLETTER_CONTENT = `${API_CONTENT_URI}/getContent/?name=newsletter&type=component`;
export const GET_RESET_PASSWORD_CONTENT = `${API_CONTENT_URI}/getContent/?name=resetPassword&type=component`;
export const GET_UPDATE_PASSWORD_CONTENT = `${API_CONTENT_URI}/getContent/?name=updatePassword&type=component`;
export const GET_SIGNIN_CONTENT = `${API_CONTENT_URI}/getContent/?name=userSignin&type=component`;
export const GET_SIGNUP_CONTENT = `${API_CONTENT_URI}/getContent/?name=userSignup&type=component`;
export const GET_SIGNOUT_CONTENT = `${API_CONTENT_URI}/getContent/?name=userSignout&type=component`;
export const GET_TESTIMONIALS_CONTENT = `${API_CONTENT_URI}/getContent/?name=testimonials&type=component`;
export const GET_UNSUBSCRIBE_CONTENT = `${API_CONTENT_URI}/getContent/?name=unsubscribe&type=component`;
export const GET_ACTIVATE_ACCOUNT_CONTENT = `${API_CONTENT_URI}/getContent/?name=activateAccount&type=component`;
export const GET_UPDATE_NEWSLETTER_CONTENT = `${API_CONTENT_URI}/getContent/?name=updateNewsletter&type=component`;
export const GET_WRONG_PAGE_PROMPT_CONTENT = `${API_CONTENT_URI}/getContent/?name=wrongPagePrompt&type=component`;
export const GET_ACCOUNT_CONTENT = `${API_CONTENT_URI}/getContent/?name=account&type=component`;
export const GET_STORY_CONTENT = `${API_CONTENT_URI}/getContent/?name=story&type=document`;
export const GET_TERMS_CONTENT = `${API_CONTENT_URI}/getContent/?name=terms&type=document`;
export const GET_POLICY_CONTENT = `${API_CONTENT_URI}/getContent/?name=policy&type=document`;
export const GET_SHOWCASE_CONTENT = `${API_CONTENT_URI}/getContent/?name=showcase&type=document`;

export const GET_ARTICLE_MAIN_IMAGE_URL = `${API_ASSETS_URI}/getArticleAsset/?id={id}&assetName=image.jpg`;

export const GET_IMAGES_URL = `${API_NON_VIDEO_ASSETS_URI}/?blobName=images`;
export const GET_ARTICLE_IMAGE_URL = `${API_NON_VIDEO_ASSETS_URI}/?blobName=images/sections/articles`;
export const GET_FEATURED_IMAGE_URL = `${API_NON_VIDEO_ASSETS_URI}/?blobName=images/sections/featured`;
export const GET_TESTIMONIALS_URL = `${API_NON_VIDEO_ASSETS_URI}/?blobName=images/sections/testimonials`;
export const GET_ICONS_URL = `${API_NON_VIDEO_ASSETS_URI}/?blobName=images/icons`;

export const MAIN_ICON = `${GET_ICONS_URL}/main_logo.svg`;
export const MEDIUM_ICON = `${GET_ICONS_URL}/medium_icon.svg`;
export const ARTICLE_PATH = "/articles/?id={id}";
