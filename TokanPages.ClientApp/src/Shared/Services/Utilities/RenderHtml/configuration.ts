import { BLOCK_TAG, LINK_TAGS, LIST_TAGS, QUOTE_TAGS, TABLE_TAGS, TEXT_TAGS } from "./constants";

const ALLOWED_TAGS = Array.prototype.concat(BLOCK_TAG, TEXT_TAGS, QUOTE_TAGS, LINK_TAGS, LIST_TAGS, TABLE_TAGS);

export const configuration = {
    ALLOWED_TAGS: ALLOWED_TAGS,
    FORBID_TAGS: ["style"],
    FORBID_ATTR: ["style"],
    ALLOW_ARIA_ATTR: false,
    ALLOW_DATA_ATTR: false
}
