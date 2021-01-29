// The module 'vscode' contains the VS Code extensibility API
// Import the module and reference it with the alias vscode in your code below
import * as vscode from 'vscode';

// this method is called when your extension is activated
// your extension is activated the very first time the command is executed
export function activate(context: vscode.ExtensionContext) {
	let NEXT_TERM_ID = 1;
	// Use the console to output diagnostic information (console.log) and errors (console.error)
	// This line of code will only be executed once when your extension is activated
	console.log('Congratulations, your extension "texttemplating" is now active!');

	// The command has been defined in the package.json file
	// Now provide the implementation of the command with registerCommand
	// The commandId parameter must match the command field in package.json
	let disposable = vscode.commands.registerCommand('tt.helloWorlds', () => {
		// The code you place here will be executed every time your command is executed

		// Display a message box to the user
		vscode.window.showInformationMessage('Hello World from texttemplating!');
		
	});
	context.subscriptions.push(vscode.commands.registerCommand('tt.helloWorld',  (uri:vscode.Uri)  => {
		const terminal = vscode.window.createTerminal(`TT #${NEXT_TERM_ID++}`);
		terminal.sendText(`dotnet tt '${uri.fsPath}'`);
		terminal.show();
	}));
	context.subscriptions.push(vscode.commands.registerCommand('terminalTest.show', () => {
		if (ensureTerminalExists()) {
			selectTerminal().then(terminal => {
				if (terminal) {
					terminal.show();
				}
			});
		}
	}));
 //https://github.com/microsoft/vscode-extension-samples/blob/master/terminal-sample/src/extension.ts
	context.subscriptions.push(disposable);
}

// this method is called when your extension is deactivated
export function deactivate() {}
function colorText(text: string): string {
	let output = '';
	let colorIndex = 1;
	for (let i = 0; i < text.length; i++) {
		const char = text.charAt(i);
		if (char === ' ' || char === '\r' || char === '\n') {
			output += char;
		} else {
			output += `\x1b[3${colorIndex++}m${text.charAt(i)}\x1b[0m`;
			if (colorIndex > 6) {
				colorIndex = 1;
			}
		}
	}
	return output;
}

function selectTerminal(): Thenable<vscode.Terminal | undefined> {
	interface TerminalQuickPickItem extends vscode.QuickPickItem {
		terminal: vscode.Terminal;
	}
	const terminals = <vscode.Terminal[]>(<any>vscode.window).terminals;
	const items: TerminalQuickPickItem[] = terminals.map(t => {
		return {
			label: `name: ${t.name}`,
			terminal: t
		};
	});
	return vscode.window.showQuickPick(items).then(item => {
		return item ? item.terminal : undefined;
	});
}

function ensureTerminalExists(): boolean {
	if ((<any>vscode.window).terminals.length === 0) {
		vscode.window.showErrorMessage('No active terminals');
		return false;
	}
	return true;
}
