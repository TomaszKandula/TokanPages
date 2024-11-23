import { useDispatch } from "react-redux";
import { ApplicationLanguageAction } from "../../Store/Actions";
import { GetContentManifestDto, LanguageItemDto } from "../../Api/Models";

export const GetDefaultId = (items: LanguageItemDto[]): string | undefined => {
    for (let index in items) {
        if (items[index].isDefault) {
            return items[index].id;
        }
    }

    return undefined;
};

export const UpdateUserLanguage = (manifest: GetContentManifestDto | undefined): void => {
    if (manifest === undefined) return;

    const languages = manifest.languages;
    const defaultId = GetDefaultId(languages);

    if (defaultId !== undefined) {
        const dispatch = useDispatch();
        dispatch(ApplicationLanguageAction.set({ id: defaultId, languages: languages }));
    }
};
