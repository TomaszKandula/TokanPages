import { ContentPageDataState } from "../../../../Store/States";

export const ComponentsCheck = (components: string[], state: ContentPageDataState, languageId: string): string[] | undefined => {
    let result: string[] = [];

    components.forEach(item => {
        if (Object.hasOwn(state.components, item)){
            const key = item as keyof typeof state.components
            const componentLanguage = state.components[key].language;
            const isLanguageChanged = componentLanguage != languageId;
            if (isLanguageChanged) {
                result.push(item)
            }
        }
    });

    return result.length === 0 ? undefined : result;
}
