import { ContentPageDataState } from "../../../../Store/States";
import { ComponentsDto, ContentModelDto } from "../../../../Api/Models";

const UpdateComponent = (state: ComponentsDto, data: ContentModelDto): ComponentsDto => {
    let result: ComponentsDto = { ...state };
    const contentName = data.contentName ?? "";
    if (Object.hasOwn(result, contentName)) {
        const key = contentName as keyof typeof result;
        if (data.content) {
            // @ts-expect-error
            // NOTE: We expect object content to match
            // content DTO model, but we do not cast it.
            result[key] = data.content;
        }
    }

    return result;
};

export const UpdateComponents = (state: ContentPageDataState, source: ContentModelDto[]): ComponentsDto => {
    let result: ComponentsDto = { ...state.components };

    source.forEach(item => {
        result = UpdateComponent(result, item);
    });

    return result;
};
