import { ContentPageDataState } from "../../../../Store/States";

interface ComponentsCheckProps {
    components: string[];
    state: ContentPageDataState;
    languageId: string;
}

export const GetVerifiedComponents = (props: ComponentsCheckProps): string[] | undefined => {
    let result: string[] = [];

    props.components.forEach(item => {
        if (Object.hasOwn(props.state.components, item)) {
            const key = item as keyof typeof props.state.components;
            const componentLanguage = props.state.components[key].language;
            const isLanguageChanged = componentLanguage != props.languageId;
            if (isLanguageChanged) {
                result.push(item);
            }
        }
    });

    return result.length === 0 ? undefined : result;
};
