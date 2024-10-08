export type ContentType = "component" | "document";
export interface ContentModelDto {
    contentType?: ContentType;
    contentName?: string;
    content?: object;
}
