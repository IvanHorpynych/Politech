import java.io.BufferedReader;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.ArrayList;
import java.util.Properties;
import java.util.Random;

public class Password {
	public static final int MIN_LENGTH = 4;
	
	//settings fields
	private int length;
	private int varNumber;
	private int maxLength;
	private int minLength;
	private boolean withSymbols;
	private boolean withDigits;
	private boolean withShifts;
	private String vocabulary;
	private String symbols;
	
	//main data fields
	private String basis;
	private ArrayList<String> variants;
	
	//default constructor
	public Password(String settings) {
		variants = new ArrayList<String>();
		loadSettings(settings);
	}
	
	//select random word from vocabulary file
	public void generateBasis(){
		String s = null;
		try {
			//computing the number of lines in vocabulary file
			int length = 0;
			BufferedReader in = new BufferedReader(new InputStreamReader(new FileInputStream(vocabulary)));
			while (in.ready()) {
				in.readLine();
				length++;
			}
			in.close();
			
			//select random line, if we can't find needed word, select another line
			boolean ok = false;
			while (!ok) {
				in = new BufferedReader(new InputStreamReader(new FileInputStream(vocabulary)));
				Random random = new Random();
				int position = random.nextInt(length);
				for (int i = 0; i < position-1; i++) {
					in.readLine();
				}
				s = in.readLine();
				in.close();
				
				//select word in this line with length from 6 to 8 symbols
				int l = 0;
				int flag = 0;
				char ch[] = s.toCharArray();
				for (int i = 0; i < ch.length; i++) {
					if ((ch[i] >= 'a')&&(ch[i] <= 'z') || (ch[i] >= 'A')&&(ch[i] <= 'Z')) {
						l++;
						if (flag == 0) flag = 1;
					} else {
						if ((l >= minLength) && (l <= maxLength)) {
							s = String.copyValueOf(ch, i-l, l);
						}
						l = 0;
						flag = 0;
					}
				}
								
				//final check of selected word
				if (s.length() != ch.length) {
					ok = true;
				}
			}
		} catch (IOException e) {
			basis = "Error on loading vocabulary file!";
		}
		basis = s;
	}
	
	//generate variants for basis
	public void generateVariants() {
		variants.clear();
		for (int i = 0; i < varNumber; i++) {
			generateVariant();
		}
	}
	public void generateVariant() {
		int freeSpace = length - basis.length();
		String variant = basis;
		Random random = new Random();
		int k = 0;
		int ins = 0;
		//add shifts to password
		if (withShifts) {
			k = random.nextInt(2);
			for (int j = 0; j < k; j++) {
				ins = random.nextInt(variant.length());
				if ((variant.charAt(ins) >= 'a') && (variant.charAt(ins) <= 'z')) {
					String shift = variant.substring(ins, ins+1);
					shift = shift.toUpperCase();
					variant = replaceChar(variant, shift.charAt(0), ins);
				}
			}
		}
		//add special symbols
		if (withSymbols && (freeSpace > 0)) {
			do {
				k = random.nextInt(3);
			} while (k > freeSpace);
			for (int j = 0; j < k; j++) {
				ins = random.nextInt(variant.length());
				variant = insertChar(variant, symbols.charAt(random.nextInt(symbols.length())), ins);
			}
			freeSpace -= k;
		}
		//add digits
		if (withDigits && (freeSpace > 0)) {
			for (int j = 0; j < freeSpace; j++) {
				ins = random.nextInt(variant.length());
				variant = insertChar(variant, ((Integer)random.nextInt(10)).toString().charAt(0), ins);
			}
		}
		variants.add(variants.size()+") "+variant);
	}
	
	//string operations
	private String insertChar(String s, char c, int pos) {
		StringBuffer buf = new StringBuffer(s);
		buf.insert(pos, c);
		return buf.toString();
	}
	private String replaceChar(String s, char c, int pos) {
		StringBuffer buf = new StringBuffer(s);
		buf.deleteCharAt(pos);
		buf.insert(pos, c);
		return buf.toString();
	}
	
	//save-load settings
	private void loadSettings(String src) {
		Properties settings = new Properties();
		try {
			settings.load(new FileInputStream(src));
			withSymbols = Boolean.parseBoolean(settings.getProperty("withSymbols"));
			withDigits = Boolean.parseBoolean(settings.getProperty("withDigits"));
			withShifts = Boolean.parseBoolean(settings.getProperty("withShifts"));
			vocabulary = settings.getProperty("vocabulary");
			varNumber = Integer.parseInt(settings.getProperty("varNumber"));
			length = Integer.parseInt(settings.getProperty("length"));
			minLength = Integer.parseInt(settings.getProperty("min_length"));
			maxLength = Integer.parseInt(settings.getProperty("max_length"));
			symbols = settings.getProperty("symbols");
		} catch (IOException e) {
			basis = "Error on loading configuration file!";
		}
	}
	public void saveSettings(String src) {
		Properties settings = new Properties();
		try {
			settings.setProperty("withSymbols", ((Boolean)withSymbols).toString());
			settings.setProperty("withDigits", ((Boolean)withDigits).toString());
			settings.setProperty("withShifts", ((Boolean)withShifts).toString());
			settings.setProperty("vocabulary", vocabulary);
			settings.setProperty("varNumber", ((Integer)varNumber).toString());
			settings.setProperty("length", ((Integer)length).toString());
			settings.setProperty("min_length", ((Integer)minLength).toString());
			settings.setProperty("max_length", ((Integer)maxLength).toString());
			settings.setProperty("symbols", symbols);
			settings.store(new FileOutputStream(src), "");
		} catch (IOException e) {
			basis = "Error on saving configuration!";
		}
	}
	
	//getters
	public String getBasis() {
		return basis;
	}
	public String getVocabulary() {
		return vocabulary;
	}
	public String getSymbols() {
		return symbols;
	}
	public String getVariant(int index) {
		return variants.get(index);
	}
	public boolean getWithSymbols() {
		return withSymbols;
	}
	public boolean getWithDigits() {
		return withDigits;
	}
	public boolean getWithShifts() {
		return withShifts;
	}
	public int getVarNumber() {
		return varNumber;
	}
	public int getLength() {
		return length;
	}
	public int getMinLength() {
		return minLength;
	}
	public int getMaxLength() {
		return maxLength;
	}
	
	//setters
	public void setBasis(String basis) {
		this.basis = basis;
	}
	public void setVocabulary(String vocabulary) {
		this.vocabulary = vocabulary;
	}
	public void setSymbols(String symbols) {
		this.symbols = symbols;
	}
	public void setWithSymbols(boolean withSymbols) {
		this.withSymbols = withSymbols;
	}
	public void setWithDigits(boolean withDigits) {
		this.withDigits = withDigits;
	}
	public void setWithShifts(boolean withShifts) {
		this.withShifts = withShifts;
	}
	public void setVarNumber(int varNumber) {
		this.varNumber = varNumber;
	}
	public void setLength(int length) {
		this.length = length;
	}
	public void setMinLength(int minLength) {
		this.minLength = minLength;
	}
	public void setMaxLength(int maxLength) {
		this.maxLength = maxLength;
	}
}
