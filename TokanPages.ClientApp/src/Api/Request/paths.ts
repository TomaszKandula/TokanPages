const API_VER = process.env.REACT_APP_API_VER;
const APP_BACKEND = process.env.REACT_APP_BACKEND;
const API_BASE_URI = `${APP_BACKEND}/api/v${API_VER}`;
const API_ARTICLES_URI = `${API_BASE_URI}/articles`;
const API_USERS_URI = `${API_BASE_URI}/users`;
const API_SUBSCRIBERS_URI = `${API_BASE_URI}/subscribers`;
const API_MAILER_URI = `${API_BASE_URI}/mailer`;
const API_CONTENT_URI = `${API_BASE_URI}/content`;
const API_ASSETS_URI = `${API_BASE_URI}/assets`;

export const GET_ARTICLES = `${API_ARTICLES_URI}/getAllArticles`;
export const GET_ARTICLE = `${API_ARTICLES_URI}/getArticle/{id}`;
export const ADD_ARTICLE = `${API_ARTICLES_URI}/addArticle`;
export const UPDATE_ARTICLE_CONTENT = `${API_ARTICLES_URI}/updateArticleContent`;
export const UPDATE_ARTICLE_COUNT = `${API_ARTICLES_URI}/updateArticleCount`;
export const UPDATE_ARTICLE_LIKES = `${API_ARTICLES_URI}/updateArticleLikes`;
export const UPDATE_ARTICLE_VISIBILITY = `${API_ARTICLES_URI}/updateArticleVisibility`;
export const REMOVE_ARTICLE = `${API_ARTICLES_URI}/removeArticle`;

export const GET_USERS = `${API_USERS_URI}/getAllUsers`;
export const GET_USER = `${API_USERS_URI}/getUser/{id}`;
export const ADD_USER = `${API_USERS_URI}/addUser`;
export const ACTIVATE_USER = `${API_USERS_URI}/activateUser`;
export const UPDATE_USER = `${API_USERS_URI}/updateUser`;
export const REMOVE_USER = `${API_USERS_URI}/removeUser`;
export const AUTHENTICATE = `${API_USERS_URI}/authenticateUser`;
export const REAUTHENTICATE = `${API_USERS_URI}/reAuthenticateUser`;
export const RESET_USER_PASSWORD = `${API_USERS_URI}/resetUserPassword`;
export const UPDATE_USER_PASSWORD = `${API_USERS_URI}/updateUserPassword`;

export const GET_SUBSCRIBERS = `${API_SUBSCRIBERS_URI}/getAllSubscribers`;
export const GET_SUBSCRIBER = `${API_SUBSCRIBERS_URI}/getSubscriber/{id}`;
export const ADD_SUBSCRIBER = `${API_SUBSCRIBERS_URI}/addSubscriber`;
export const UPDATE_SUBSCRIBER = `${API_SUBSCRIBERS_URI}/updateSubscriber`;
export const REMOVE_SUBSCRIBER = `${API_SUBSCRIBERS_URI}/removeSubscriber`;

export const VERIFY_EMAIL = `${API_MAILER_URI}/verifyEmailAddress`;
export const SEND_MESSAGE = `${API_MAILER_URI}/sendMessage`;
export const SEND_NEWSLETTER = `${API_MAILER_URI}/sendNewsletter`;

export const GET_CONTENT_MANIFEST = `${API_CONTENT_URI}/getContentManifest`;
export const GET_NAVIGATION_CONTENT = `${API_CONTENT_URI}/getContent/?name=navigation&type=component`;
export const GET_HEADER_CONTENT = `${API_CONTENT_URI}/getContent/?name=header&type=component`;
export const GET_FOOTER_CONTENT = `${API_CONTENT_URI}/getContent/?name=footer&type=component`;
export const GET_ARTICLE_FEAT_CONTENT = `${API_CONTENT_URI}/getContent/?name=articleFeatures&type=component`;
export const GET_CONTACT_FORM_CONTENT = `${API_CONTENT_URI}/getContent/?name=contactForm&type=component`;
export const GET_COOKIES_PROMPT_CONTENT = `${API_CONTENT_URI}/getContent/?name=cookiesPrompt&type=component`;
export const GET_CLIENTS_CONTENT = `${API_CONTENT_URI}/getContent/?name=clients&type=component`;
export const GET_FEATURED_CONTENT = `${API_CONTENT_URI}/getContent/?name=featured&type=component`;
export const GET_FEATURES_CONTENT = `${API_CONTENT_URI}/getContent/?name=features&type=component`;
export const GET_NEWSLETTER_CONTENT = `${API_CONTENT_URI}/getContent/?name=newsletter&type=component`;
export const GET_RESET_PASSWORD_CONTENT = `${API_CONTENT_URI}/getContent/?name=resetPassword&type=component`;
export const GET_UPDATE_PASSWORD_CONTENT = `${API_CONTENT_URI}/getContent/?name=updatePassword&type=component`;
export const GET_SIGNIN_CONTENT = `${API_CONTENT_URI}/getContent/?name=userSignin&type=component`;
export const GET_SIGNUP_CONTENT = `${API_CONTENT_URI}/getContent/?name=userSignup&type=component`;
export const GET_SIGNOUT_CONTENT = `${API_CONTENT_URI}/getContent/?name=userSignout&type=component`;
export const GET_TESTIMONIALS_CONTENT = `${API_CONTENT_URI}/getContent/?name=testimonials&type=component`;
export const GET_UNSUBSCRIBE_CONTENT = `${API_CONTENT_URI}/getContent/?name=unsubscribe&type=component`;
export const GET_ACTIVATE_ACCOUNT_CONTENT = `${API_CONTENT_URI}/getContent/?name=activateAccount&type=component`;
export const GET_UPDATE_SUBSCRIBER_CONTENT = `${API_CONTENT_URI}/getContent/?name=updateSubscriber&type=component`;
export const GET_WRONG_PAGE_PROMPT_CONTENT = `${API_CONTENT_URI}/getContent/?name=wrongPagePrompt&type=component`;
export const GET_ACCOUNT_CONTENT = `${API_CONTENT_URI}/getContent/?name=account&type=component`;
export const GET_STORY_CONTENT = `${API_CONTENT_URI}/getContent/?name=myStory&type=document`;
export const GET_TERMS_CONTENT = `${API_CONTENT_URI}/getContent/?name=terms&type=document`;
export const GET_POLICY_CONTENT = `${API_CONTENT_URI}/getContent/?name=policy&type=document`;

export const GET_IMAGE_URL = `${API_ASSETS_URI}/getArticleAsset/?id={id}&assetName=image.jpg`;
export const GET_IMAGES_URL = `${API_ASSETS_URI}/getAsset/?blobName=images`;
export const GET_ARTICLE_IMAGE_URL = `${API_ASSETS_URI}/getAsset/?blobName=images/sections/articles`;
export const GET_FEATURED_IMAGE_URL = `${API_ASSETS_URI}/getAsset/?blobName=images/sections/featured`;
export const GET_TESTIMONIALS_URL = `${API_ASSETS_URI}/getAsset/?blobName=images/sections/testimonials`;
export const GET_ICONS_URL = `${API_ASSETS_URI}/getAsset/?blobName=images/icons`;
export const GET_AVATARS_URL = `${API_ASSETS_URI}/getAsset/?blobName=images/avatars`;

export const MAIN_ICON = `${GET_ICONS_URL}/main_logo.svg`;
export const MEDIUM_ICON = `${GET_ICONS_URL}/medium_icon.svg`;
export const ARTICLE_PATH = `/articles/?id={id}`;
