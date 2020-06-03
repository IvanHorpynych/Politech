import java.awt.event.*;
import javax.swing.*;

public class PassGen extends JFrame {
	private static final long serialVersionUID = 1L;
	private static final String SETTINGS = ".settings";
	private static final int X_BUTTON = 100;
	private static final int Y_BUTTON = 40;
	private static final int X_LABEL = 100;
	private static final int Y_LABEL = 20;
	private static final int X_FRAME = 315;
	private static final int Y_FRAME = 290;
		
	//main area
	private JLabel passLabel;
	private JLabel varLabel;
	private JButton genButton;
	private JButton varButton;
	private JButton addVarButton;
	private JButton settButton;
	private JButton aboutButton;
	private JTextArea varTA;
	private JTextField passTF;
	private JScrollPane varScrollPane;
	//setting area
	private JLabel settLabel;
	private JLabel vocLabel;
	private JLabel symbLabel;
	private JLabel varNumLabel;
	private JLabel lengthLabel;
	private JLabel minLengthLabel;
	private JLabel maxLengthLabel;
	private JButton OKButton;
	private JButton CancelButton;
	private JCheckBox symbolsBox;
	private JCheckBox digitsBox;
	private JCheckBox shiftsBox;
	private JTextField vocTF;
	private JTextField symbTF;
	private JTextField varNumTF;
	private JTextField lengthTF;
	private JTextField minLengthTF;
	private JTextField maxLengthTF;
	//about area
	private JTextArea aboutTA;
	//data fields		
	private Password password = null;
	private boolean aboutPressed = false;
	
	public PassGen() {
		super("Safe Password Generator v1.0");
		setSize(X_FRAME, Y_FRAME);
		setResizable(false);
		setLocationByPlatform(true);
		setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		getContentPane().setLayout(null);
		password = new Password(SETTINGS);
		
		//main area
		passLabel = new JLabel("Password basis:");
		passLabel.setBounds(10, 10, X_LABEL, Y_LABEL);
			add(passLabel);
		varLabel = new JLabel("Variants:");
		varLabel.setBounds(10, 50, X_LABEL, Y_LABEL);
			add(varLabel);
		passTF = new JTextField();
		passTF.setBounds(10, 30, 180, Y_LABEL);
			add(passTF);
		varTA = new JTextArea();
		varTA.setBounds(10, 70, 180, 180);
			add(varTA);
		varScrollPane = new JScrollPane(varTA);
		varScrollPane.setBounds(10, 70, 180, 180);
			add(varScrollPane);	
		genButton = new JButton("Basis");
		genButton.setBounds(200, 10, X_BUTTON, Y_BUTTON);
		genButton.addActionListener(new ActionListener() {
			public void actionPerformed(ActionEvent e) {
				password.generateBasis();
				passTF.setText(password.getBasis());
			}
		});
			add(genButton);
		varButton = new JButton("Variants");
		varButton.setBounds(200, 60, X_BUTTON, Y_BUTTON);
		varButton.addActionListener(new ActionListener() {
			public void actionPerformed(ActionEvent e) {
				if (passTF.getText() != null) {
					password.setBasis(passTF.getText());
					varTA.setText("");
					password.generateVariants();
					int number = password.getVarNumber();
					for (int i = 0; i < number; i++) {
						varTA.append(password.getVariant(i)+"\n");
					}
				}
			}
		});
			add(varButton);
		addVarButton = new JButton("Add variant");
		addVarButton.setBounds(200, 110, X_BUTTON, Y_BUTTON);
		addVarButton.addActionListener(new ActionListener() {
			public void actionPerformed(ActionEvent e) {
				if (password.getBasis() != null) {
					password.setVarNumber(password.getVarNumber()+1);
					password.generateVariant();
					varTA.append(password.getVariant(password.getVarNumber()-1)+"\n");
					restoreSettings();
				}
			}
		});
			add(addVarButton);
		settButton = new JButton("Settings");
		settButton.setBounds(200, 160, X_BUTTON, Y_BUTTON);
		settButton.addActionListener(new ActionListener() {
			public void actionPerformed(ActionEvent e) {
				aboutPressed = false;
				setSize(535, Y_FRAME);
			}
		});
			add(settButton);
		aboutButton = new JButton("About");
		aboutButton.setBounds(200, 210, X_BUTTON, Y_BUTTON);
		aboutButton.addActionListener(new ActionListener() {
			public void actionPerformed(ActionEvent e) {
				if (aboutPressed) {
					setSize(X_FRAME, Y_FRAME);
					aboutPressed = false;
				} else {
					setSize(X_FRAME, 350);
					aboutPressed = true;
				}
				restoreSettings();
			}
		});
			add(aboutButton);
			
		//setting area
		settLabel = new JLabel("Settings");
		settLabel.setBounds(390, 5, X_LABEL, Y_LABEL);
			add(settLabel);
		vocLabel = new JLabel("Vocabulary");
		vocLabel.setBounds(310, 30, X_LABEL, Y_LABEL);
			add(vocLabel);
		symbLabel = new JLabel("Symbols");
		symbLabel.setBounds(310, 50, X_LABEL, Y_LABEL);
			add(symbLabel);
		varNumLabel = new JLabel("Variants");
		varNumLabel.setBounds(310, 70, X_LABEL, Y_LABEL);
			add(varNumLabel);
		lengthLabel = new JLabel("Password length");
		lengthLabel.setBounds(310, 90, X_LABEL, Y_LABEL);
			add(lengthLabel);
		minLengthLabel = new JLabel("Basis length: from");
		minLengthLabel.setBounds(310, 110, X_LABEL+10, Y_LABEL);
			add(minLengthLabel);
		maxLengthLabel = new JLabel("to");
		maxLengthLabel.setBounds(450, 110, 30, Y_LABEL);
			add(maxLengthLabel);
		vocTF = new JTextField(password.getVocabulary());
		vocTF.setBounds(420, 30, X_LABEL, Y_LABEL);
			add(vocTF);
		symbTF = new JTextField(password.getSymbols());
		symbTF.setBounds(420, 50, X_LABEL, Y_LABEL);
			add(symbTF);
		varNumTF = new JTextField(((Integer)password.getVarNumber()).toString());
		varNumTF.setBounds(420, 70, X_LABEL, Y_LABEL);
			add(varNumTF);
		lengthTF = new JTextField(((Integer)password.getLength()).toString());
		lengthTF.setBounds(420, 90, X_LABEL, Y_LABEL);
			add(lengthTF);
		minLengthTF = new JTextField(((Integer)password.getMinLength()).toString());
		minLengthTF.setBounds(420, 110, Y_LABEL, Y_LABEL);
			add(minLengthTF);
		maxLengthTF = new JTextField(((Integer)password.getMaxLength()).toString());
		maxLengthTF.setBounds(470, 110, Y_LABEL, Y_LABEL);
			add(maxLengthTF);
		symbolsBox = new JCheckBox("Use special symbols.");
		symbolsBox.setBounds(320, 135, 180, Y_LABEL);
		if (password.getWithSymbols()) {
			symbolsBox.doClick();
		}
			add(symbolsBox);
		digitsBox = new JCheckBox("Use digits.");
		digitsBox.setBounds(320, 155, 180, Y_LABEL);
		if (password.getWithDigits()) {
			digitsBox.doClick();
		}
			add(digitsBox);
		shiftsBox = new JCheckBox("Use shifts.");
		shiftsBox.setBounds(320, 175, 180, Y_LABEL);
		if (password.getWithShifts()) {
			shiftsBox.doClick();
		}
			add(shiftsBox);
		OKButton = new JButton("OK");
		OKButton.setBounds(310, 210, X_BUTTON, Y_BUTTON);
		OKButton.addActionListener(new ActionListener () {
			public void actionPerformed(ActionEvent e) {
				setSize(X_FRAME, Y_FRAME);
				saveSettings();
				password.saveSettings(SETTINGS); 
			}
		});
			add(OKButton);
		CancelButton = new JButton("Cancel");
		CancelButton.setBounds(320+X_BUTTON, 210, X_BUTTON, Y_BUTTON);
		CancelButton.addActionListener(new ActionListener() {
			public void actionPerformed(ActionEvent e) {
				setSize(X_FRAME, Y_FRAME);
				restoreSettings();				
			}
		});
			add(CancelButton);
			
		//about area
		aboutTA = new JTextArea("Safe Password Generator v1.0\nmade by Polishchuk Andriy\nNTUU KPI, FAM, KV-81");
		aboutTA.setBounds(10, 260, X_FRAME-25, 50);
		aboutTA.setEditable(false);
			add(aboutTA);
	}
	
	//save settings from text fields to password object
	private void saveSettings() {
		if (symbolsBox.isSelected()) password.setWithSymbols(true);
			else password.setWithSymbols(false);
		if (digitsBox.isSelected()) password.setWithDigits(true);
			else password.setWithDigits(false);
		if (shiftsBox.isSelected()) password.setWithShifts(true);
			else password.setWithShifts(false);
		password.setVocabulary(vocTF.getText());
		password.setSymbols(symbTF.getText());
		if (Integer.parseInt(varNumTF.getText()) > 0) {
			password.setVarNumber(Integer.parseInt(varNumTF.getText()));
		} else restoreSettings();
		if ((Integer.parseInt(lengthTF.getText()) > Password.MIN_LENGTH) && 
			(Integer.parseInt(maxLengthTF.getText()) <= Integer.parseInt(lengthTF.getText())) &&
			(Integer.parseInt(maxLengthTF.getText()) > 0) &&
			(Integer.parseInt(minLengthTF.getText()) <= Integer.parseInt(maxLengthTF.getText())) &&
			(Integer.parseInt(minLengthTF.getText()) > 0)) {
			password.setLength(Integer.parseInt(lengthTF.getText()));
			password.setMinLength(Integer.parseInt(minLengthTF.getText()));
			password.setMaxLength(Integer.parseInt(maxLengthTF.getText()));
		} else restoreSettings();
	}
	
	//restore text fields in settings area
	private void restoreSettings() {
		if (password.getWithSymbols() != symbolsBox.isSelected()) { 
			symbolsBox.doClick(); 
		}
		if (password.getWithDigits() != digitsBox.isSelected()) { 
			digitsBox.doClick(); 
		}
		if (password.getWithShifts() != shiftsBox.isSelected()) { 
			shiftsBox.doClick(); 
		}
		vocTF.setText(password.getVocabulary());
		symbTF.setText(password.getSymbols());
		varNumTF.setText(((Integer)password.getVarNumber()).toString());
		lengthTF.setText(((Integer)password.getLength()).toString());
		minLengthTF.setText(((Integer)password.getMinLength()).toString());
		maxLengthTF.setText(((Integer)password.getMaxLength()).toString());
	}
	
	public static void main(String[] args) {
		PassGen passgen = new PassGen();
		passgen.setVisible(true);
	}
}