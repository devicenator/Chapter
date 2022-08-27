# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/)

## [Unreleased]
## [1.0.0] - 2022-08-27
### Changed
Rebrand to Chapter

## [2.0.0] - 2022-07-22
### Changed
* Upgrade to net6

## [1.2.4] - 2022-05-19
### Added
* AsyncEventHandler and extensions for it

## [1.2.3] - 2021-11-27
### Added
* Handling for similag processes to the ProcessHandler
* ProcessHandler is able to launch any file
* ProcessHandler is able to launch another application and returns it runtime data

## [1.2.2] - 2021-09-17
### Changed
* Removed objects out of the Validation namespace

## [1.2.1] - 2021-08-13
### Fixed
* The ObservableObject NotifyAndSetIf and NotifyAndSetIfChanged crashed if the backing field is a null string

## [1.2.0] - 2021-08-01
### Added
* NotifyDataErrorInfo for an easy handling of input validations
* ValidatableObservableObject, a new ObservableObject base class which implements the NotifyDataErrorInfo for a more easy usage
* AsyncValidatableObservableObject, a ValidatableObservableObject but in this case the validation overload is already a method with a Task return.

## [1.1.0] - 2021-07-17
### Added
* NameOf (Build name of inclusive class and namespace)
* Add copy or move directory over partitions

## [1.0.0] - 2021-06-13
### Added
* AsyncDelegateCommand (an ICommand with an async callback)
* DelegateCommand (an ICommand with a callback)
* EventArgs (Generic object for event arguments)
* ObjectEx (Extensions for the object type)
* ObservableObject (A base class for INotifyPropertyChanging and INotifyPropertyChanged)
* ProcessHandler (Helper methods to work with processes)
* TaskExtensions (Extensions for the task type)
* WorkingIndicator (Indicator if an parallel work is still ongoing)
