# TagManager

Tag mangaging tool that allows a user to manage the tags assigned to media items in a media catalog. 
This application was developed to enable users to manage and process raw collect tag data that, for example was gathered using crows-sourcing.

The user is able to browse a catalog and select a media item. In case of a video, the user is able to view it in a video player. 
The details-page of the item also shows the associated (raw) tags. 
When loading the processing step, the raw data is preprocessing by checking for incorrectly spelled tags.
Before starting the real processing step, the user has the possibility to ignore or edit specific tags in the preprocessed step.
During the actual processing step, the data is grouped by unique values and further processed by looking for synonyms, and existing tags in the repository.
The resulting set that is returned to the user contains the groups that eventually will result in MediaTags. 
These tags are potentially used to generate the search context (additionally by proving a media tag type). 
For each resulting group, the user has the option to select one of the corresponding raw tags to be used as the value for the media tag. 
In case an existing MediaTag was found in the repository, the user is informed by the information-icon, that shows the MediaTag data in a tooltip when hovering over it.

When the user is finished, he is able to save the working set of MediaTags and return to the catalog item details page.
After the processing is done, the details page will show the relevant media items in the catalog by looking at items that have multiple matching MediaTags.
