const API_VER = process.env.REACT_APP_API_VER;

/* BASE URL */

export const APP_FRONTEND = process.env.REACT_APP_FRONTEND;
export const APP_BACKEND  = process.env.REACT_APP_BACKEND;
export const APP_STORAGE  = process.env.REACT_APP_STORAGE;

/* API | ARTICLES */

export const API_QUERY_GET_ARTICLES     = `${APP_BACKEND}/api/v${API_VER}/articles/GetAllArticles/`;
export const API_QUERY_GET_ARTICLE      = `${APP_BACKEND}/api/v${API_VER}/articles/GetArticle/{id}/`;
export const API_COMMAND_ADD_ARTICLE    = `${APP_BACKEND}/api/v${API_VER}/articles/AddArticle/`;
export const API_COMMAND_UPDATE_ARTICLE = `${APP_BACKEND}/api/v${API_VER}/articles/UpdateArticle/`;
export const API_COMMAND_REMOVE_ARTICLE = `${APP_BACKEND}/api/v${API_VER}/articles/RemoveArticle/`;

/* API | USERS */

export const API_QUERY_GET_USERS     = `${APP_BACKEND}/api/v${API_VER}/users/GetAllUsers/`;
export const API_QUERY_GET_USER      = `${APP_BACKEND}/api/v${API_VER}/users/GetUser/{id}/`;
export const API_COMMAND_ADD_USER    = `${APP_BACKEND}/api/v${API_VER}/users/AddUser/`;
export const API_COMMAND_UPDATE_USER = `${APP_BACKEND}/api/v${API_VER}/users/UpdateUser/`;
export const API_COMMAND_REMOVE_USER = `${APP_BACKEND}/api/v${API_VER}/users/RemoveUser/`;

/* API | SUBSCRIBERS */

export const API_QUERY_GET_SUBSCRIBERS     = `${APP_BACKEND}/api/v${API_VER}/subscribers/GetAllSubscribers/`;
export const API_QUERY_GET_SUBSCRIBER      = `${APP_BACKEND}/api/v${API_VER}/subscribers/GetSubscriber/{id}/`;
export const API_COMMAND_ADD_SUBSCRIBER    = `${APP_BACKEND}/api/v${API_VER}/subscribers/AddSubscriber/`;
export const API_COMMAND_UPDATE_SUBSCRIBER = `${APP_BACKEND}/api/v${API_VER}/subscribers/UpdateSubscriber/`;
export const API_COMMAND_REMOVE_SUBSCRIBER = `${APP_BACKEND}/api/v${API_VER}/subscribers/RemoveSubscriber/`;

/* API | MAILER */

export const API_COMMAND_VERIFY_EMAIL    = `${APP_BACKEND}/api/v${API_VER}/mailer/VerifyEmailAddress/`;
export const API_COMMAND_SEND_MESSAGE    = `${APP_BACKEND}/api/v${API_VER}/mailer/SendMessage/`;
export const API_COMMAND_SEND_NEWSLETTER = `${APP_BACKEND}/api/v${API_VER}/mailer/SendNewsletter/`;

/* CONTENT */

export const STORY_URL   = `${APP_STORAGE}/content/mystory.json`;
export const TERMS_URL   = `${APP_STORAGE}/content/terms.json`;
export const POLICY_URL  = `${APP_STORAGE}/content/policy.json`;
export const ARTICLE_URL = `${APP_STORAGE}/content/articles/{ID}/text.json`;
export const IMAGE_URL   = `${APP_STORAGE}/content/articles/{ID}/image.jpg`;

/* PATHS */

export const ARTICLE_PATH      = `/articles/?id={ID}`;
export const IMAGES_PATH       = `${APP_STORAGE}/images/`;
export const TESTIMONIALS_PATH = `${APP_STORAGE}/images/section_testimonials/`;
export const ICONS_PATH        = `${APP_STORAGE}/icons/`;
export const AVATARS_PATH      = `${APP_STORAGE}/content/avatars/`;

/* MESSAGE TEMPLATES */

export const MESSAGE_OUT_SUCCESS = `<p>Congratulations!</p><p>The message has been sent successfully, we will try to respond as soon as possible.</p>`;
export const MESSAGE_OUT_WARNING = `<span>We have received following warning(s):</span><ul>{LIST}</ul><span>To send an email all fields must be filled along with acceptance of Terms of Use and Privacy Policy.</span>`;
export const MESSAGE_OUT_ERROR   = `<p>Ouch!</p><p>The message could not be sent.</p><p>{ERROR}.</p>`;

export const NEWSLETTER_SUCCESS = `<p>Congratulations!</p><p>You have been added to the newsletter.</p>`;
export const NEWSLETTER_WARNING = `<span>The newsletter could not be setup, following warning(s) received:</span><ul>{LIST}</ul><span>Please make sure the email is valid and you have access to it.</span>`;
export const NEWSLETTER_ERROR   = `<p>Ouch!</p><p>The newsletter could not be setup.</p><p>{ERROR}.</p>`;

export const UPDATE_ARTICLE_SUCCESS = `<p>Success!</p><p>Article has been updated successfully</p>`;
export const UPDATE_ARTICLE_WARNING = `<span>We have received following warning(s):</span><ul>{LIST}</ul><span>Please check if all required fields are filled correctly.</span>`;
export const UPDATE_ARTICLE_ERROR   = `<p>Ouch!</p><p>The article could not be updated.</p><p>{ERROR}.</p>`;

export const SUBSCRIBER_DEL_ERROR = `<p>We could not remove your email from our newsletter list.</p><p{ERROR}.</p><p>Please contact us to resolve the issue.</p>`;

export const UNEXPECTED_STATUS = `Received unexpected status code: {STATUS_CODE}. Please contact IT Support`;
export const UNEXPECTED_ERROR  = `Unexpected error`;
